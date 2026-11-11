<script setup>
import axios from 'axios';
import { onMounted, ref } from 'vue';
import { RouterLink } from 'vue-router';
import { formatDate } from '@/utility/formatDate';

const user = ref(JSON.parse(localStorage.getItem('userData')) || null);
const date = new Date();
const baseUrl = import.meta.env.VITE_BACKEND_URL;

const posts = ref(null);

onMounted(async () => {
    await GetPosts();
})

async function GetPosts() {
    const response = await axios.get(`${baseUrl}/Post?uuid=${user.value.uuid}`)
    console.log(response);
    console.table(await response.data)
    posts.value = await response.data;
}

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
            <RouterLink class="q-link" :to="{ name: auth - post }">Posts</RouterLink>
            <RouterLink class="q-link" :to="{ name: auth - home }">Profile</RouterLink>
            <RouterLink class="q-link" :to="{ name: auth - home }">Logout</RouterLink>
        </div>
    </section>
    <section class="section-container col-2">
        <div class="col-2-container">
            <h2 style="place-self: center;">Posts</h2>
            <table>
                <thead>
                    <tr>
                        <th>Image</th>
                        <th>Title</th>
                        <th>Slug</th>
                        <th>Status</th>
                        <th>Created at</th>
                        <th>Updated at</th>
                        <th>Published at</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="post in posts" :key="post.id">
                        <td><img :src="post.coverImageUrl" alt=""></td>
                        <td>{{ post.title }}</td>
                        <td>{{ post.slug }}</td>
                        <td>{{ post.status }}</td>
                        <td>{{ formatDate(post.createdAt) }}</td>
                        <td>{{ formatDate(post.updatedAt) }}</td>
                        <td>{{ formatDate(post.publishedAt) }}</td>
                    </tr>
                </tbody>
            </table>

        </div>
        <div class="col-2-container">
            <h2 style="place-self: center;">Followers</h2>
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
    transition: 0.3s;
    border: 1px solid var(--border);
    padding: 0.5rem 1rem;
    border-radius: .5rem;
}

.q-link:hover {
    border-color: var(--text);
}

.col-2 {
    display: grid;
    grid-template-columns: 1fr 1fr;
    place-items: center;
    gap: 1rem;
}

.col-2-container {
    height: fit-content;
    min-height: 100%;
}
</style>