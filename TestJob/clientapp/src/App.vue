<template>
    <div id="app" class="wrapper">
        <div class="container-fluid">
            <div class="row">
                <nav class="col-md-2 d-none d-md-block bg-light sidebar">
                    <main-menu></main-menu>
                </nav>
                <main class="col-md-9 ml-sm-auto col-lg-10 px-4" role="main">
                    <router-view />
                </main>
            </div>
            
        </div>
    </div>
</template>
<script>
    import MainMenu from './components/MainMenu.vue';
    export default {
        components: {
            MainMenu
        }
    }
</script>
