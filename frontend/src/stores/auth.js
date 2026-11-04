import axios from "axios";
import { defineStore } from "pinia";
import {ref, computed} from "vue";

export const useAuthStore = defineStore('auth', () => {
    const user = ref(JSON.parse(localStorage.getItem('userData')) || null);
    const token = ref(localStorage.getItem('token') || null)

    const isAuthenticated = computed(() => !!token.value)

    const baseUrl = import.meta.env.VITE_BACKEND_URL;

    function setAuthData(userData, jwtToken) {
        user.value = userData;
        token.value = jwtToken;
        localStorage.setItem('userData', JSON.stringify(userData));
        localStorage.setItem('token', jwtToken);
    }

    async function login(credentials) {
        try {
            const { data } = await axios.post(`${baseUrl}/Auth/login`, credentials);
            setAuthData(data.user, data.token);
            return true;
        } catch (error) {
            console.error(error);
            return false;
        }
    }

    function logout() {
        user.value = null;
        token.value = null;
        localStorage.removeItem('token');
        localStorage.removeItem('userData');
        if (localStorage.getItem('token') === null) {
            console.log('credentials removed');
            return true;
        }
    }

    return {
        user,
        token,
        isAuthenticated,
        login,
        logout
    };
});