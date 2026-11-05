<script setup>
import { ref } from 'vue';
import { RouterLink } from 'vue-router';

const user = ref(JSON.parse(localStorage.getItem('userData')) || null);
const date = new Date();



</script>

<template>
    <section class="section-container">
        <div id="top-one">
            <h1>Welcome {{ user.firstName }}</h1>
            <p class="text-muted">{{ date.getDate() }}.{{ date.getMonth() }}.{{ date.getFullYear() }}</p>
        </div>
        <div id="top-two" class="text-muted">
            {{ user.username }} &#183; {{ user.email }} &#183; {{ user.uuid }}
        </div>
    </section>
    <section class="section-container">
        <h2 id="quick-link-header">Quick links</h2>
        <div id="quick-cta-container">
            <RouterLink class="q-link" :to="{ name: auth - home }">Media Bank</RouterLink>
            <RouterLink class="q-link" :to="{ name: auth - home }">Posts</RouterLink>
            <RouterLink class="q-link" :to="{ name: auth - home }">Profile</RouterLink>
            <RouterLink class="q-link" :to="{ name: auth - home }">Logout</RouterLink>
        </div>
    </section>
    <section class="section-container">

    </section>
</template>

<style scoped>
section {
    display: flex;
    flex-direction: column;
    width: 100%;
    height: fit-content;
    padding: 5dvh 5dvw 0 5dvw;
}

.section-container {
    display: flex;
    flex-direction: column;
    width: 100%;
    height: fit-content;
    gap: 1rem;
    margin-bottom: 5dvh;
}

#top-one {
    display: flex;
    width: 100%;
    justify-content: space-between;
    align-items: center;
}

.text-muted {
    color: var(--muted);
}

#quick-cta-container {
    display: flex;
    width: 100%;
    justify-content: space-evenly;
}

#quick-link-header {
    place-self: center;
}

.q-link {
    text-decoration: underline;
    text-decoration-color: transparent;
    transition: 0.3s;
}

.q-link:hover {
    text-decoration-color: var(--text);
}
</style>