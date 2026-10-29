<script setup>
import { RouterLink, useRoute } from 'vue-router';
const route = useRoute()
</script>

<template>
    <header v-if="!route.meta.hideNav">
        <nav>
            <RouterLink id="nav-logo" to="/">[ the common room ]</RouterLink>
            <div id="nav-item-container">
                <RouterLink class="nav-item" to="/">home</RouterLink>
                <RouterLink class="nav-item" to="/articles">articles</RouterLink>
                <RouterLink class="nav-item" to="/bloggers">bloggers</RouterLink>
            </div>
            <div id="nav-buttons-container">
                <RouterLink class="nav-item" id="nav-login" to="/login">login</RouterLink>
                <RouterLink id="nav-create-accout" to="/create-account">create account</RouterLink>
            </div>
        </nav>
    </header>
    <main>
        <RouterView v-slot="{ Component }">
            <Transition name="fade" mode="out-in">
                <component :is="Component" />
            </Transition>
        </RouterView>
    </main>
    <footer>

    </footer>
</template>

<style scoped>
/* transition */
.fade-enter-active,
.fade-leave-active {
    transition: opacity .2s ease;
}

.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}

/*transition end*/
header {
    width: 100%;
    height: 10dvh;
    margin: 1rem 0 .5rem 0;
    display: flex;
    justify-content: center;
}

nav {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    width: 75%;
    height: auto;
    background-color: var(--surface);
    padding: 0 2rem 0 2rem;
    border-radius: 1rem;
    box-shadow: rgba(149, 157, 165, 0.2) 0px 8px 24px;
}

#nav-logo {
    align-self: center;
}

#nav-item-container,
#nav-buttons-container {
    display: flex;
    gap: 1rem;
    justify-content: center;
    align-items: center;
}

#nav-buttons-container {
    justify-content: end !important;
}

.nav-item {
    text-decoration: underline;
    text-decoration-color: transparent;
    transition: .5s;
}

.nav-item:hover {
    text-decoration-color: var(--muted);
}

#nav-create-accout {
    padding: 1rem;

    border-radius: 1rem;

    transition: .5s;
}

#nav-create-accout:hover {
    color: var(--bg);
    background-color: var(--accent);
}

main {
    width: 100dvw;
    height: auto;
}
</style>
