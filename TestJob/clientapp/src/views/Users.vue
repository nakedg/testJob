<template>
    <div>
        {{users}}
    </div>
</template>
<script>
    export default {
        data() {
            return {
                users: [
                    {
                        id: 1,
                        name: 'name'
                    }
                ]
            }
        }
    }
</script>