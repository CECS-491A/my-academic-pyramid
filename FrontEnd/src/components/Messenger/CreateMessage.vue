<template>
    <div class="container" style="margin-bottom: 30px">
        <v-toolbar
        card
        color="light-blue"
        dark>
        <v-toolbar-title>Replying</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-icon large color="blue darken-2">chat</v-icon>
        </v-toolbar>
        <v-form @submit.prevent="createMessage">
            <v-text-field 
            v-model="conversation.messageContent" 
            label="Message" 
            outline 
            clearable ></v-text-field>
                <p class="text-danger" v-if="errorText">{{ errorText }}</p>
            <button class="btn btn-primary" type="submit" name="action">
                <v-btn color="success">Send</v-btn>
            </button>
        </v-form>



    </div>
</template>

<script>

    export default {
        name: 'CreateMessage',
        props: ['name'],
        data(){
            return {
                newMessage:{
					SenderId: "2",
					ContactUsername: "",
					messageContent:""
                },
                currentConversationId,
                errorText: null
            }
        },
        created() {
            this.$eventBus.$on("LoadMessageContact",conversationId =>{
                this.currentConversationId = conversationId
            })
           
        },
        methods: {
            
        }
    }
</script>