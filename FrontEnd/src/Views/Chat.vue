<template>
  <div id="chatApp">
    <!-- Create a chat toolbar with 2 tab -->
    <v-app id="chat">
      <v-container fluid grid-list-xl>
        <v-layout row wrap>
          <v-flex xs12 md4>
            <v-tabs left color="cyan" dark icons-and-text>
              <v-tabs-slider color="green"></v-tabs-slider>
              <!-- Create first tab that hold chat history c -->
              <v-tab id="chatHistory">Chat History</v-tab>
              <v-tab-item>
                <v-card height="350px">
                  <v-toolbar color="teal" dark>
                    <v-list class="pa-0">                      
                      <v-btn
                      id = 'newMsgButton'
                        fab
                        medium
                        color="red"
                        bottom
                        right
                        absolute
                        @click="newMessageDialog = !newMessageDialog"
                      >
                        <v-icon>edit</v-icon>
                      </v-btn>
                    </v-list>
                  </v-toolbar>
                  <!-- Create list for conversation. Current selected conversation will have blue color-->
                  <v-list two-line>
                    <v-divider></v-divider>
                    <v-list-tile
                      v-for="item in conversations"
                      :key="item.Id"
                      :color="selectedConversationId!= item.Id ? 'black' : 'blue'" 
                      @click="getMessageInConversation(item.Id)"
                    >
                      <v-list-tile-content>
                        <v-list-tile-title>
                          {{ item.ContactUsername }}</v-list-tile-title>
                        <v-icon :color="item.HasNewMessage && selectedConversationId!= item.Id ? 'red' : 'grey'">chat_bubble</v-icon>
                        <v-list-tile-sub-title>{{item.CreatedDate}}</v-list-tile-sub-title>
                      </v-list-tile-content>

                      <!-- Create an floating window containing a button to delete conversation -->
                      <v-list-tile-action>
                        <v-menu
                        max-height="100px"
                         bottom right>
                          <template v-slot:activator="{ on }">
                            <v-btn dark icon v-on="on" color="black">
                              <v-icon>more_vert</v-icon>
                            </v-btn>
                          </template>
                          <v-card >
                            <v-list>
                              <v-list-tile avatar>
                                <v-list-tile-avatar>
                                  <img src="https://cdn.vuetifyjs.com/images/john.jpg" alt="John">
                                </v-list-tile-avatar>

                                <v-list-tile-content>
                                  <v-list-tile-title
                                 >{{item.ContactUsername}}</v-list-tile-title>
                                  <v-list-tile-sub-title></v-list-tile-sub-title>
                                </v-list-tile-content>

                                <!-- Create a button to delete conversation -->
                                <v-list-tile-action>
                                  <v-btn icon @click="DeleteConversation(item.Id)">
                                    <v-icon>delete</v-icon>
                                  </v-btn>
                                </v-list-tile-action>
                              </v-list-tile>
                            </v-list>
                          </v-card>
                        </v-menu>
                      </v-list-tile-action>
                    </v-list-tile>
                  </v-list>
                </v-card>
              </v-tab-item>

              <!-- Make friend list as second tab -->
              <v-tab id="friendList">Friend List</v-tab>
              <v-tab-item>
                <friendList></friendList>
              </v-tab-item>
            </v-tabs>
          </v-flex>
          <v-flex xs12 md6>
            <v-card class="elevation-12">
              <chatBox></chatBox>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>

      <!-- Create a dialog used to start a new conversation -->
      <v-dialog 
      v-model="newMessageDialog" 
      max-width="500px">
        <v-card>
          <form ref="newMessageDialog">
            <!-- Create text field for receiver username -->
          <v-card-text>
              <v-text-field 
              id ="receiverUsername"
            label="Receiver Username" 
            v-model="newMessage.contactUsername"
            :error-messages="usernameErrors"
            required
            @input="$v.newMessage.contactUsername.$touch()"
            @blur="$v.newMessage.contactUsername.$touch()"
            ></v-text-field>
          
          <!-- Another text field for message -->
            <v-text-field 
            id = "inputMessage"
            label="Message" 
            v-model="newMessage.messageContent"
            :error-messages="messageErrors"
            required
            @input="$v.newMessage.messageContent.$touch()"
            @blur="$v.newMessage.messageContent.$touch()"
            ></v-text-field>
            <!-- Show error  -->
            <v-alert :value="error" type="error" transition="scale-transition">{{error}}</v-alert>
          </v-card-text>
          </form>
          <v-card-actions>
            <v-spacer></v-spacer>

            <!-- Create a button to send message -->
            <v-btn
            id ="sendNewMsgButton" 
            flat color="primary" 
            @click="sendMessageWithNewConversation">Submit</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-app>
  </div>
</template>

<script>
import chatBox from "@/components/Messenger/ChatBox";
import friendList from "@/components/Messenger/FriendList";
import { validationMixin } from "vuelidate";
import { required, email } from "vuelidate/lib/validators";
export default {
  mixins: [validationMixin],

// Validation for receiver username and message text file
  validations: {
    newMessage: {
      contactUsername: {required,email},
      messageContent: {required}
    }
  },
  components: {
    chatBox,
    friendList
  },
  data() {
    return {
      conversations: [],
      newMessageDialog: false,
      newMessage: {
        contactUsername: "",
        messageContent: ""
      },

      // Variable that hold the current selected conversation
      selectedConversationId: "",

      // Hold the returned message error from api 
      error:"",

      // Used to trigger the alert 
      alert: false,
      
    };
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
  watch:{
    selectedConversationId(){
      this.$eventBus.$emit("GetMessageInConversation", this.selectedConversationId)
    }

  },
  created() {
      this.getAllConversations(),     // Retrieve all conversation at initial

      // Listen for the send message action from friend list component
      this.$eventBus.$on("SendMessageFromFriendList", friendUsername => {
        
        this.newMessage.contactUsername = friendUsername, // Set the receiver username received from friendlist component
        this.newMessageDialog = true; // Show newMessageDialog
      });
      this.$eventBus.$on("MessageFromUserProfile", newMessage =>{
        this.$router.push("/chat");
        this.newMessage.contactUsername = newMessage.contactUsername,
        this.newMessage.messageContent = newMessage.messageContent
        this.sendMessageWithNewConversation()
      })

      // Listen to reload the conversation panels from friendlist component
    this.$eventBus.$on("ReloadChatHistoryList", () => {
      this.getAllConversations();
    });
  },
  methods: {

    async getAllConversations() { // Method to get all conversation from back end
      await this.axios({
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.SITtoken
        },
        method: "GET",
        crossDomain: true,
        url: this.$hostname + "messenger/GetAllConversation"
      })
        .then(response => { // Populate the conversations data with response data 
          this.conversations = response.data.conversations;
          //sessionStorage.SITtoken = response.data.SITtoken
        })
        .catch(err => {
          /* eslint no-console: "off" */
          console.log(err);
        });
    },

    // Method to get all message in a conversation.
    getMessageInConversation(conversationId) { 
      this.selectedConversationId = conversationId;
    },

    // Start a new conversation and send message
    async sendMessageWithNewConversation() {
      if (this.newMessage.messageContent) {
        await this.axios({
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
            Authorization: "Bearer " + sessionStorage.SITtoken
          },
          method: "POST",
          crossDomain: true,
          url: this.$hostname + "messenger/SendMessageWithNewConversation",
          data: this.newMessage
        })
          .then(response => {
            //sessionStorage.SITtoken = response.data.SITtoken
            this.newMessageDialog = false;
            this.selectedConversationId = response.data.message.ConversationId,
            this.getAllConversations();
            this.$eventBus.$emit("GetMessageInConversation", this.selectedConversationId)
           
        
          })
          .catch(err => {
            /* eslint no-console: "off" */
            if(err.response.status == 404 ){
              this.error = "User with the username does not exist"
            }
            else if(err.response.status == 409){
              this.error = "You cannot send message to yourself"
            }
            this.alert = true;
          })
      }
    },

    // To delete a conversation
    async DeleteConversation(conversationId) {
      await this.axios({
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.SITtoken
        },
        method: "DELETE",
        crossDomain: true,
        url:
          this.$hostname +
          "messenger/DeleteConversation?conversationId=" +
          conversationId
      })
        .then(() => {
          // sessionStorage.SITtoken = response.data.SITtoken
         this.getAllConversations(),
          this.$eventBus.$emit("ClearChatScreen");
        })
        .catch(err => {
          /* eslint no-console: "off" */
          console.log(err);
        });
    }
  }
};
</script>
