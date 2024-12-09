<script lang="ts">
import { defineComponent } from 'vue';
import { useRouter } from 'vue-router';

export default defineComponent({
    name: 'AdminSchedule',
    setup() {
        const router = useRouter();

        const navigateToAdmin = () => {
            router.push('/admin'); // Substitui this.$router.push
        };

        return { navigateToAdmin };
    },
});
</script>

<template>
    <div>
        <h1>Admin Schedule</h1>
        <button @click="navigateToAdmin">Ir para Admin</button>
    </div>
</template>