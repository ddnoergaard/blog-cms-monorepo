<script setup>
import router from '@/router';
import { RouterLink } from 'vue-router';

async function handleLogout() {
    //logout handling here, clear userStore and either localStorage or httpCookie
    //Would be best to use httpCookie, to prevent cross site scripting
    //i also need to create a pinia store, to store the user state. this also needs to be cleared on logout
    await router.push({ name: 'login' })
}

</script>

<template>
    <nav>
        <div id="nav-top">
            <p id="logo">[ The common room ]</p>
            <div id="nav-top-link-container">
                <RouterLink :to="{ name: auth - home }">Dashboard</RouterLink>
                <RouterLink :to="{ name: auth - home }">Posts</RouterLink>
            </div>

        </div>
        <div id="nav-bottom">
            <span class="separator"></span>
            <RouterLink :to="{ name: auth - home }">Profile</RouterLink>
            <button @click="handleLogout">Logout</button>
            <div id="profile-card">
                <img src="/favicon.ico" alt="">
                <div id="pc-text">
                    <p id="pc-t-name">ddnoergaard</p>
                    <p id="pc-t-desc" style=" color: var(--muted);">Followers: 100</p>
                </div>
            </div>
        </div>

    </nav>
</template>

<style scoped>
nav {
    display: flex;
    width: 10dvw;
    height: 90dvh;
    position: sticky;
    flex-direction: column;
    justify-content: space-between;
    align-items: start;
    background-color: var(--accent);
    padding: 5dvh 1dvw 5dvh 1dvw;
}

p,
a {
    color: var(--bg);
}

a {
    text-decoration: underline;
    text-decoration-color: transparent;
    transition: 0.3s;
}

a:hover {
    text-decoration-color: var(--bg);
}

#nav-top {
    display: flex;
    flex-direction: column;
    gap: 2rem;
    width: 100%;
}

#nav-top-link-container {
    display: flex;
    flex-direction: column;
    gap: 1rem;
}

#logo {
    place-self: center;
}

#nav-bottom {
    display: flex;
    flex-direction: column;
    gap: 1rem;
    width: 100%;
}

.separator {
    display: block;
    content: "";
    height: 1px;
    width: 100%;
    background-color: var(--bg);
}

#profile-card {
    display: flex;
    gap: 1rem;
}

#pc-text {
    display: flex;
    flex-direction: column;
    justify-content: center;

    text-align: start;
}

#pc-t-desc {
    font-size: 0.8rem;
}
</style>