<template>
    <div>
        <loading :active.sync="isLoading" :color="'#007bff'"></loading>
        <div class="row">
            <div class="col">
                <h3>Список пользователей</h3>
            </div>
        </div>
        
        <div class="row m-2">
            <div class="col">
                <input class="form-control" type="text" placeholder="поиск + фильтр" v-model="filter" />
            </div>
            <div class="col">
                <button class="btn btn-primary" @click="addUser">Добавить пользователя</button>
            </div>
        </div>
        <div class="row">
            <b-table id="user-table"
                     :fields="userFields"
                     :items="users"
                     :sort-by="sortDefenition.fieldName"
                     :sort-desc="sortDefenition.isDesc"
                     @sort-changed="sortChanged"
                     :no-local-sorting="true"
                     :striped="true"
                     head-variant="dark">
                <template slot="actions" slot-scope="data">
                    <div class="dropdown">
                        <button class="btn btn-default" type="button" data-toggle="dropdown">
                            <i class="fas fa-bars"></i>
                        </button>
                        <div class="dropdown-menu">
                            <router-link :to="'/user/edit/' + data.item.id" class="dropdown-item">
                                Редактировать
                            </router-link>
                            <button class="dropdown-item" type="button" @click="deleteUser(data.item)">Удалить</button>
                        </div>
                    </div>
                </template>
            </b-table>
        </div>
        <div class="row">
            <div class="col">
                <b-pagination :page="page"
                              :total-rows="totalCount"
                              :per-page="pageSize"
                              aria-controls="user-table"
                              @change="pageChanged"></b-pagination>
            </div>
            <div class="col form-inline">
                <label>К-во элементов на странице:</label>
                <select class="form-control" v-model="pageSize">
                    <option v-for="s in pageSizes" :value="s">{{s}}</option>
                </select>
            </div>
        </div>
        
    </div>
</template>
<script>

    import userService from '../../services/userService';
    import Loading from 'vue-loading-overlay';

    export default {
        components: {
            Loading
        },
        data() {
            return {
                page: 1,
                pageSize: 10,
                pageSizes: [10, 20, 30, 50, 100],
                totalCount: 0,
                filter: '',
                users: [
                    {
                        id: 1,
                        name: 'name'
                    }
                ],
                isLoading: false,
                userFields: [
                    {
                        key: 'actions',
                        label: ''
                    },
                    {
                        key: 'id',
                        label: 'id',
                        sortable: true
                    },
                    {
                        key: 'login',
                        label: 'Логин',
                        sortable: true
                    },
                    {
                        key: 'lastName',
                        label: 'Фамилия',
                        sortable: true
                    },
                    {
                        key: 'firstName',
                        label: 'Имя',
                        sortable: true
                    },
                    {
                        key: 'email',
                        label: 'E-mail',
                        sortable: true
                    }
                ],
                sortDefenition: {
                    fieldName: 'login',
                    isDesc: false
                }
            }
        },
        mounted() {
            this.loadData();
        },
        methods: {
            pageChanged(page) {
                this.page = page;
                this.loadData();
            },
            sortChanged(ctx) {
                this.sortDefenition.fieldName = ctx.sortBy;
                this.sortDefenition.isDesc = ctx.sortDesc;
                this.page = 1;
                this.loadData();

            },
            loadData() {
                this.isLoading = true;
                userService.getUsers(this.page, this.pageSize, this.sortDefenition, this.filter)
                    .then(users => {
                        this.users = users.items;
                        this.totalCount = users.totalCount;
                    })
                    .catch(error => {
                        console.log(error);
                    })
                    .finally(() => {
                        this.isLoading = false;
                    })
            },
            addUser() {
                this.$router.push({ path: '/user/add' });
            },
            deleteUser(user) {
                this.$bvModal.msgBoxConfirm(`Вы действительно хотите удалить пользователя с логином "${user.login}"?`)
                    .then((result) => {
                        if (result) {
                            userService.deleteUser(user.id)
                                .then(u => {
                                    this.$bvToast.toast(`Пользователь с логином ${u.login} был удален.`, {
                                        title: 'Успешно',
                                        autoHideDelay: 5000,
                                        variant: 'success',
                                        appendToast: true
                                    });
                                    this.loadData();
                                });
                        }
                    });
            }
        },
        watch: {
            filter() {
                //debounce implementation
                if (this.timeoutId) {
                    clearTimeout(this.timeoutId);
                }
                this.timeoutId = setTimeout(() => {
                    this.loadData();
                }, 500);
                
            },
            pageSize() {
                this.page = 1;
                this.loadData();
            }
        }

    }
</script>