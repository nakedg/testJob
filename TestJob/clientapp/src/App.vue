<template>
    <div id="app" class="wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-2">
                    <ul class="nav flex-column">
                        <li class="nav-item">
                            <router-link to="/" class="nav-link">Home</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link to="/about" class="nav-link">About</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link to="/users" class="nav-link">Пользователи</router-link>
                        </li>
                    </ul>
                </div>
                <main class="col-md-10">
                    <router-view />
                </main>
            </div>
            
        </div>
    </div>
</template>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
