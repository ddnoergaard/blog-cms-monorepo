<script setup>
import axios from 'axios';
import { ref } from 'vue';
import { RouterLink } from 'vue-router';

const baseUrl = import.meta.env.VITE_BACKEND_URL;

var displayList = ref([]);
const newArticles = axios.get(baseUrl + 'api/articles?new=true');
const popularArticles = ref([]);

</script>

<template>
    <section id="hero-section">
        <div id="hs-text">
            <h1>Somewhere to share. Something to read.</h1>
            <p>free blogs for anyone with a story to tell, and plenty to read when you’d rather just listen.</p>
        </div>
        <div id="hs-btn-container">
            <RouterLink class="hs-btn" to="/login">start writing</RouterLink>
            <RouterLink class="hs-btn" to="/articles">see whats being read</RouterLink>
        </div>
    </section>
    <section id="second-container">
        <div id="overview">
            <h2>new articles</h2>
            <h2>popular articles</h2>
            <div>
                <div></div>
            </div>
        </div>
        <div id="top-b-container"></div>
    </section>
</template>

<style scoped>
#hero-section {
    display: flex;
    width: 100dvw;
    height: 90dvh;
    flex-direction: column;
    gap: 2rem;
    justify-content: center;
    align-items: center;
}

#hs-text {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    gap: 1rem;
}

#hs-btn-container {
    display: flex;
    gap: 2rem;
}

.hs-btn {
    padding: 1rem;
    transition: .5s;
    border-radius: 1rem;
    border: 1px solid transparent;
}

.hs-btn:hover {
    border-color: var(--accent);
}

#second-container {
    display: flex;

}
</style>