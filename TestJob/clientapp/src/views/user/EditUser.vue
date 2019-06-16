<template>
    <form>
        <loading :active.sync="isLoading" :color="'#007bff'"></loading>
        <div class="form-group">
            <label for="login">Логин</label>
            <input type="text" class="form-control" id="login" name="login" v-model="user.login" v-validate="'required'" :class="getValidateClasses('login')" />
        </div>
        <div class="form-group">
            <label for="lastName">Фамилия</label>
            <input type="text" class="form-control" id="lastName" name="lastName" v-model="user.lastName" v-validate="'required'" :class="getValidateClasses('lastName')" />
        </div>
        <div class="form-group">
            <label for="firstName">Имя</label>
            <input type="text" class="form-control" id="firstName" name="firstName" v-model="user.firstName" v-validate="'required'" :class="getValidateClasses('firstName')" />
        </div>
        <div class="form-group">
            <label for="email">E-mail</label>
            <input type="text" class="form-control" id="email" name="email" v-model="user.email" v-validate="'required|email'" :class="getValidateClasses('email')" />
        </div>
        <div>
            <button class="btn btn-default" @click.prevent="cancel">Отмена</button>
            <button class="btn btn-primary" :disabled="isSaving || formInvalid()" @click.prevent="save">Сохранить</button>
        </div>
    </form>
</template>
<script>
    import userService from '../../services/userService';
    import Loading from 'vue-loading-overlay';
    import Vue from 'vue';

    export default {
        components: {
            Loading
        },
        inject: [
            '$validator'
        ],
        data() {
            return {
                isLoading: false,
                isSaving: false,
                user: {
                    id: 0,
                    login: '',
                    firstName: '',
                    lastName: '',
                    email: ''
                }
            }
        },
        mounted() {
            if (this.$route.params.id) {
                this.loadData(this.$route.params.id);
            }
        },
        methods: {
            save() {
                this.isSaving = true;
                let promise = null;
                if (this.user.id > 0) {
                    promise = userService.editUser(this.user);
                } else {
                    promise = userService.createUser(this.user);
                }

                promise
                    .then(u => {
                        this.$router.push({ path: '/user/list' });
                        Vue.nextTick(() => {
                            this.$bvToast.toast('Пользователь успешно сохранен.', {
                                title: 'Успешно',
                                autoHideDelay: 5000,
                                variant: 'success',
                                appendToast: true
                            });
                        });
                        
                    })
                    .catch(error => {
                        this.$bvToast.toast('Произошла ошибка.', {
                            title: 'Ошибка',
                            autoHideDelay: 5000,
                            variant: 'danger',
                            appendToast: true
                        });
                    })
                    .finally(() => {
                        this.isSaving = false;
                    });
            },
            cancel() {
                if (history.length > 0) {
                    this.$router.back();
                } else {
                    this.$router.push({ path: '/user/list' });
                }
            },
            getValidateClasses(fieldName) {
                const matcher = {
                    name: fieldName
                };

                let valid = false;
                let invalid = false;

                const field = this.$validator.fields.find(matcher);
                if (field) {
                    valid = field.flags.valid === true && field.flags.dirty;
                    invalid = field.flags.invalid === true;
                }

                return {
                    'is-valid': valid,
                    'is-invalid': invalid
                };
            },
            formInvalid(scope) {
                if (scope) {
                    const matcher = {
                        scope
                    };
                    const fields = this.$validator.fields.filter(matcher);
                    return fields.some((field) => field.flags.invalid === true);
                } else {
                    return this.$validator.fields.items.some((field) => field.flags.invalid === true);
                }
            },
            loadData(id) {
                this.isLoading = true;
                userService.getUser(id)
                    .then(u => {
                        this.user = u;
                    })
                    .catch(error => {
                        if (error && error.status === 404) {
                            this.$bvToast.toast('Пользователь не найден.', {
                                title: 'Ошибка',
                                autoHideDelay: 5000,
                                variant: 'danger',
                                appendToast: true
                            });
                        }
                        else {
                            this.$bvToast.toast('Произошла ошибка.', {
                                title: 'Ошибка',
                                autoHideDelay: 5000,
                                variant: 'danger',
                                appendToast: true
                            });
                        }
                    })
                    .finally(() => {
                        this.isLoading = false;
                    });
                
            }
        }
    }
</script>