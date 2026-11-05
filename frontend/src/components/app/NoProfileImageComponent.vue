<script setup>

const user = JSON.parse(localStorage.getItem('userData')) || null

const firstNameArray = user.firstName.slice()
const firstLetter = firstNameArray[0];
const lastNameArray = user.lastName.slice();
const lastLetter = lastNameArray[0];

</script>

<template>
    <div id="imageContainer" v-if='user.profileImage != ""'>
        <img :src="user.profileImage" alt="">
    </div>
    <div id="letterImage" v-if='user.profileImage === ""'>
        <p>{{ firstLetter }}{{ lastLetter }}</p>
    </div>
</template>

<style scoped>
#letterImage {
    display: flex;
    place-items: center;
    background-color: var(--bg);
    aspect-ratio: 1 / 1;
    border-radius: 50%;
    padding: 1dvh;
}

#imageContainer {
    display: flex;
    place-items: center;
    aspect-ratio: 1 / 1;
}

p {
    color: var(--text);
}

img {
    width: 2dvw;
    aspect-ratio: 1 / 1;
    place-self: center;
    border-radius: 50%;
}
</style>