<script setup>
import { ref } from 'vue';

const user = ref(JSON.parse(localStorage.getItem('userData')) || null);
const date = new Date();



</script>

<template>
    <section id="top">
        <div id="top-one">
            <h1>Welcome {{ user.firstName }}</h1>
            <p class="text-muted">{{ date.getDate() }}.{{ date.getMonth() }}.{{ date.getFullYear() }}</p>
        </div>
        <div id="top-two" class="text-muted">
            {{ user.username }} &#183; {{ user.email }} &#183; {{ user.uuid }}
        </div>
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

#top {
    display: flex;
    flex-direction: column;
    width: 100%;
    height: fit-content;
    gap: 1rem;
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
</style>