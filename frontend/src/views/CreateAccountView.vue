<script setup>
import { ref } from 'vue';

//const baseUrl = import.meta.env.VITE_BAKCEND_URL

const newUser = ref({
    firstName: ref(null),
    lastName: ref(null),
    email: ref(null),
    password: ref(null),
    passwordCheck: ref(null),
    username: ref(null)
})


</script>

<template>
    <div id="site-container">
        <div id="input-container">
            <h1>welcome</h1>
            <div class="input-div">
                <input type="text" v-model="newUser.firstName" placeholder="First name">
                <input type="text" v-model="newUser.lastName" placeholder="Last name">
            </div>
            <div class="input-div">
                <input type="email" v-model="newUser.email" placeholder="Email">
                <input type="text" v-model="newUser.username" placeholder="Username">
            </div>
            <div class="input-div">
                <input type="password" v-model="newUser.password" placeholder="Password">
                <input type="password" v-model="newUser.passwordCheck" placeholder="Confirm Password">
            </div>
            <div id="cta-container">
                <button class="cta" @click="createAccout">Create user</button>
                <p>|</p>
                <RouterLink to="/login" class="cta" id="create-accout-link">Already a user?</RouterLink>
            </div>
        </div>
        <div id="side-container">
            <RouterLink class="muted-text hover-highlight" to="/">&larr; home</RouterLink>
            <div id="side-main">
                <div>
                    <h2 class="white-text">lets get you sorted!</h2>
                    <p class="white-text" style="width: 75%;">create your account, and get you started writing or
                        reading.</p>
                </div>
                <p class="muted-text">any issues? feel free to contact me using the form <RouterLink to="/contact"
                        style="text-decoration: underline;" class="muted-text hover-highlight">
                        here</RouterLink>
                </p>
            </div>
            <div id="nav-container">
                <RouterLink to="/" class="muted-text hover-highlight">home</RouterLink>
                <span class="muted-text">&#183;</span>
                <RouterLink to="articles" class="muted-text hover-highlight">articles</RouterLink>
                <span class="muted-text">&#183;</span>
                <RouterLink to="/bloggers" class="muted-text hover-highlight">bloggers</RouterLink>
                <span class="muted-text">&#183;</span>
                <RouterLink to="/login" class="muted-text hover-highlight">login</RouterLink>
                <span class="muted-text">&#183;</span>
                <RouterLink to="/create-account" class="muted-text hover-highlight">create accout</RouterLink>
            </div>
        </div>
    </div>
</template>

<style scoped>
#site-container {
    display: grid;
    grid-template-columns: 1fr 1fr;
    background-color: var(--text);
}

#side-container {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    width: 100%;
    background-color: var(--text);
    padding: 1dvw 0 1dvw 1dvw;
}

#side-main {
    display: flex;
    width: 100%;
    flex-direction: column;
    gap: 5dvh;
}

#side-main>div:first-child {
    display: flex;
    flex-direction: column;
    gap: 1dvh;
}

#nav-container {
    display: flex;
    width: 100%;
    justify-content: center;
    gap: 0.5dvw;
}

.hover-highlight {
    transition: .5s;
}

.hover-highlight:hover {
    color: var(--bg);
}

.white-text {
    color: var(--bg);
}

.muted-text {
    color: var(--muted);
}

#input-container {
    height: 100dvh;
    width: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    gap: 2rem;
    background-color: var(--bg);
    border-radius: 0 5% 5% 0;
}

input {
    height: 4dvh;
    width: 13dvw;
    border: none;
    border-radius: .5rem;
    padding: 0.5rem 1rem 0.5rem 1rem;
    transition: .5s;
}

input:active {
    box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 4px;
}

h1 {
    font-size: 2rem;
}

button {

    background-color: transparent;
    transition: .5s;
    border: none;
    border-radius: 0.5rem;
    cursor: pointer;
}

button:hover {
    background-color: var(--accent);
    color: var(--bg);
}

#cta-container {
    display: flex;
    align-items: center;
}

.cta {
    padding: 0.5rem 1rem .5rem 1rem;
    font-size: 1rem;
}

#create-accout-link {
    color: var(--muted);
    transition: .5s;
}

#create-accout-link:hover {
    color: var(--text);
}

.input-div {
    display: flex;
    gap: 1rem;
}
</style>