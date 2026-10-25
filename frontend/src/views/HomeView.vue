<script setup>
//import axios from 'axios';
//import { ref } from 'vue';
import { RouterLink } from 'vue-router';
import ArticleComponent from '@/components/ArticleComponent.vue';
import TopBloggerComponent from '@/components/TopBloggerComponent.vue';
import { ref } from 'vue';

const moqList = ref([
    { id: 1, title: "test1", imageUrl: "/favicon.ico", description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin id efficitur eros. Nunc commodo dolor eu turpis ultrices, id egestas massa egestas. Donec non tempus risus", publishedAt: "25/10/2026" },
    { id: 2, title: "test2", imageUrl: "/favicon.ico", description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin id efficitur eros. Nunc commodo dolor eu turpis ultrices, id egestas massa egestas. Donec non tempus risus", publishedAt: "25/10/2026" },
    { id: 3, title: "test3", imageUrl: "/favicon.ico", description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin id efficitur eros. Nunc commodo dolor eu turpis ultrices, id egestas massa egestas. Donec non tempus risus", publishedAt: "25/10/2026" },
])

//const baseUrl = import.meta.env.VITE_BACKEND_URL;

/*var displayList = ref([]);
const newArticles = axios.get(baseUrl + 'api/articles?new=true');
const popularArticles = ref([]);
*/
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
            <div id="o-btn-container">
                <button class="overview-btn" id="btn-new-articles" @click="showNewArticles">new articles</button>
                <button class="overview-btn" id="btn-pop-articles" @click="showPopularArticles">popular
                    articles</button>
            </div>
            <div id="overview-list">
                <ArticleComponent v-for="a in moqList" :key="a.id" :title="a.title" :description="a.description"
                    :publishedAt="a.publishedAt" :imageUrl="a.imageUrl" />
            </div>
        </div>
        <div id="top-b-container">
            <TopBloggerComponent />
        </div>
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
    display: grid;
    grid-template-columns: 2fr 1fr;
    height: fit-content;
    width: 80dvw;
    margin: 0 10dvw 0 10dvw;
}

#overview {
    display: flex;
    flex-direction: column;
    justify-content: center;
}

#o-btn-container {
    display: flex;
    width: 100%;
    justify-content: space-around;
}

.overview-btn {
    width: fit-content;
    background-color: transparent;
    padding: 0.3rem 1rem 0.3rem 1rem;
    border-radius: 1rem 1rem 0 0;
}

#overview-list {
    display: grid;
    grid-template-columns: repeat(3, 3fr);
    gap: 1rem;
    width: 100%;
}
</style>