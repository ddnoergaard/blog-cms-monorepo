<script setup>

defineProps({
    title: String,
    description: String,
    publishedAt: String,
    imageUrl: String
})

</script>

<template>
    <div id="article-container">
        <img :src="imageUrl" alt="">
        <div id="article-text-container">
            <div id="a-text-top">
                <p id="title">{{ title }}</p>
                <p id="published-at">{{ publishedAt }}</p>
            </div>
            <p id="description">{{ description }}</p>
        </div>
    </div>
</template>

<style scoped>
#article-container {
    display: flex;
    gap: 1rem;
    background-color: test;
    padding: 1rem;
}

img {
    width: auto;
    height: 50%;
    place-self: center;
}

#article-text-container {
    display: flex;
    flex-direction: column;
    gap: 1rem;
}

#a-text-top {
    display: flex;
    justify-content: space-between;
    align-items: center;
}

#title {
    font-size: 1rem;
}

#published-at {
    color: var(--muted);
    font-size: 0.7rem;
}

#description {
    color: var(--muted);
    overflow: hidden;
    display: -webkit-box;
    line-clamp: 3;
    -webkit-line-clamp: 3;
    -webkit-box-orient: vertical;
}
</style>