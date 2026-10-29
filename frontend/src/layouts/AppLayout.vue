<script setup>
import NavComponent from '@/components/app/NavComponent.vue';
import { RouterView } from 'vue-router';
</script>

<template>
    <section id="main">
        <NavComponent />
        <main>
            <RouterView v-slot="{ Component }">
                <Transition name="fade" mode="out-in">
                    <component :is="Component" />
                </Transition>
            </RouterView>
        </main>
    </section>
</template>

<style scoped>
.fade-enter-active,
.fade-leave-active {
    transition: opacity .2s ease;
}

.fade-enter-from,
.fade-leave-to {
    opacity: 0;
}

#main {
    display: flex;
}
</style>