
<template>
  <v-dialog v-model="dialog"  title="Create New Conversation">
    <v-icon slot="activator" >message</v-icon>
      <v-card>
        <v-toolbar>
          <v-toolbar-title>New Conversation</v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <form>
            <v-text-field
              id="UserName"
              v-model="newMessage.contactUserName"
              :error-messages="usernameErrors"
              required
            @input="$v.newMessage.contactUsername.$touch()"
            @blur="$v.newMessage.contactUsername.$touch()"

              label="UserName (Email)"

            ></v-text-field>
            <v-text-field
              id="inputMessage"
              :error-messages="messageErrors"
              label="Message"
              v-model="newMessage.messageContent"
              required
            @input="$v.newMessage.messageContent.$touch()"
            @blur="$v.newMessage.messageContent.$touch()"

            ></v-text-field>
            <v-btn
            id ="sendNewMsgButton" 
            flat color="primary" 
            @click="sendMessage">Send</v-btn>
          </form>
        </v-card-text>
      </v-card>
  </v-dialog>
</template>
<script>
import { required, email } from "vuelidate/lib/validators";
export default {
  validations: {
    newMessage: {
      contactUsername: {required,email},
      messageContent: {required}
    }
  },
      props: {

        contactUsernameProp: {
          default:"",
          type: String

        },
        messageContentProp:{
          default:"",
          type: String

        }

  },

  data(){
    return {
        newMessage:{
          contactUsername : this.contactUsernameProp,
          messageContent : this.messageContentProp
    }
    }

  },
  computed: {
    usernameErrors() {
      const errors = [];

      if (!this.$v.newMessage.contactUsername.$dirty) return errors;

      !this.$v.newMessage.contactUsername.email && errors.push("Must be valid e-mail");
      

      !this.$v.newMessage.contactUsername.required && errors.push("E-mail is required");

      return errors;
    },
    messageErrors(){
      const errors = [];
      if (!this.$v.newMessage.messageContent.$dirty) return errors;

      !this.$v.newMessage.messageContent.required && errors.push("Message is required");
      return errors;
    }
  },
  methods: {
    sendMessage() {
      this.$eventBus.$emit("MessageFromUserProfile", this.newMessage);
      
    }
  }
};
</script>

			