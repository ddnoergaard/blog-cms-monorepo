<script setup></script>

<template>
    <main>
        <RouterView v-slot="{ Component }">
            <Transition name="fade" mode="out-in">
                <component :is="Component" />
            </Transition>
        </RouterView>
    </main>
</template>

<style scoped></style>