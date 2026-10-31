import axios from "axios";
import { defineStore } from "pinia";
import {ref, computed} from "vue";

export const useAuthStore = defineStore('auth', () => {
    const user = ref(null);
    const token = ref(localStorage.getItem('token') || null)

    const isAuthenticated = computed(() => !!token.value)

    const baseUrl = import.meta.env.VITE_BACKEND_URL;

    function setAuthData(userData, jwtToken) {
        user.value = userData;
        token.value = jwtToken;
        localStorage.setItem('token', jwtToken);
    }

    async function login(credentials) {
        try {
            const response = axios.post(baseUrl + 'auth/login', {
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify(credentials)
            });
        
            if (!response.ok) throw new Error('Login failed');

            const data = await response.JSON();

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
    }

    return {
        user,
        token,
        isAuthenticated,
        login,
        logout
    };
});