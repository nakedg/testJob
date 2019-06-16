<template>
    <div class="sidebar-sticky">
        <ul class="nav flex-column">
            <li class="nav-item">
                <router-link to="/" class="nav-link" :exact="true">Главная</router-link>
            </li>
            <li class="nav-item">
                <router-link to="/user/list" class="nav-link">Пользователи</router-link>
            </li>
        </ul>
    </div>
</template>