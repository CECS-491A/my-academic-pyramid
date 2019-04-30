<template>
  <div id="chatApp">
    <v-app id="chat">
      <v-container fluid grid-list-xl>
        <v-layout row wrap>
          <v-flex xs12 md4>
            <v-tabs left color="cyan" dark icons-and-text>
              <v-tabs-slider color="green"></v-tabs-slider>
              <v-tab id="chatHistory">Chat History</v-tab>

              <v-tab-item>
                <v-card height="350px">
                  <v-toolbar color="teal" dark>
                    <v-list class="pa-0">                      
                      <v-btn
                        fab
                        medium
                        color="red"
                        bottom
                        right
                        absolute
                        @click="chatDialog = !chatDialog"
                      >
                        <v-icon>edit</v-icon>
                      </v-btn>
                    </v-list>
                  </v-toolbar>

                  <v-list two-line>
                    <v-divider></v-divider>

                    <v-list-tile
                      v-for="item in conversations"
                      :key="item.Id"
                      :to="item.path"
                      :color="selectedConversationId!= item.Id ? 'black' : 'blue'" 
                      
                      @click="getMessageInConversation(item.Id)"
                    >
                      <v-list-tile-content>
                        <v-list-tile-title>{{ item.ContactUsername }}</v-list-tile-title>
                        <v-icon :color="item.HasNewMessage && selectedConversationId!= item.Id ? 'red' : 'grey'">chat_bubble</v-icon>
                        <!-- <v-icon v-if="item.HasNewMessage && this.selectedConversationId!= item.Id">chat_bubble</v-icon> -->
                        <v-list-tile-sub-title>{{item.CreatedDate}}</v-list-tile-sub-title>
                            
                      </v-list-tile-content>
                      <v-list-tile-action>
                        <v-menu bottom left>
                          <template v-slot:activator="{ on }">
                            <v-btn dark icon v-on="on" color="black">
                              <v-icon>more_vert</v-icon>
                            </v-btn>
                          </template>
                          <v-card>
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

                                <v-list-tile-action>
                                  <v-btn icon @click="DeleteConversation(item.Id)">
                                    <v-icon>delete</v-icon>
                                  </v-btn>
                                </v-list-tile-action>
                              </v-list-tile>
                            </v-list>

                            <v-divider></v-divider>

                            <v-card-actions>
                              <v-spacer></v-spacer>

                              <v-btn flat @click="menu = false">Cancel</v-btn>
                              <v-btn color="primary" flat @click="menu = false">Save</v-btn>
                            </v-card-actions>
                          </v-card>
                        </v-menu>
                      </v-list-tile-action>
                    </v-list-tile>
                  </v-list>
                </v-card>
              </v-tab-item>

              <v-tab id="friendList">Friend List</v-tab>

              <v-tab-item>
                <friendList></friendList>
              </v-tab-item>
            </v-tabs>
          </v-flex>
          <v-flex xs12 md6>
            <v-card class="elevation-12">
              <chat></chat>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
      <v-dialog 
      v-model="chatDialog" 
      max-width="500px">
        <v-card>
          <form ref="chatDialog">
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
          
            <v-text-field 
            id = "inputMessage"
            label="Message" 
            v-model="newMessage.messageContent"
            :error-messages="messageErrors"
            required
            @input="$v.newMessage.messageContent.$touch()"
            @blur="$v.newMessage.messageContent.$touch()"
            ></v-text-field>
            <v-alert :value="error" type="error" transition="scale-transition">{{error}}</v-alert>
          </v-card-text>
          </form>
          <v-card-actions>
            <v-spacer></v-spacer>
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
import chat from "@/components/Messenger/Chat";
import friendList from "@/components/Messenger/FriendList";
import { validationMixin } from "vuelidate";
import { required, email } from "vuelidate/lib/validators";
export default {
  mixins: [validationMixin],

  validations: {
    newMessage: {
      contactUsername: {required,email},
      messageContent: {required}
    }
  },
  components: {
    chat,
    friendList
  },
  data() {
    return {
      drawer: true,
      conversations: [],
      right: null,
      chatDialog: false,
      newMessage: {
        contactUsername: "",
        messageContent: ""
      },

      selectedConversationId: "",
      error:"",
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

  created() {
    
      this.getAllConversations(),
      this.$eventBus.$on("SendMessageFromFriendList", friendUsername => {
        (this.newMessage.contactUsername = friendUsername),
          (this.chatDialog = true);
      });

    this.$eventBus.$on("ReloadChatHistoryList", () => {
      this.getAllConversations();
    });
  },
  methods: {
   

    async getAllConversations() {
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
        .then(response => {
          this.conversations = response.data.conversations;
          //sessionStorage.SITtoken = response.data.SITtoken
        })
        .catch(err => {
          /* eslint no-console: "off" */
          console.log(err);
        });
    },

    getMessageInConversation(conversationId) {
      this.$eventBus.$emit("GetMessageInConversation", conversationId),
      this.selectedConversationId = conversationId;
    },

    

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
            this.errorText = null;
            this.chatDialog = false;
            this.selectedConversationId = response.data.message.conversationID,
            this.getAllConversations();
            // this.$eventBus.$emit(
            //   "GetMessageInConversation",
            //   this.selectedConversationId
            // );
        
          })
          .catch(err => {
            /* eslint no-console: "off" */
            if(err.response.status == 404 ){
              this.error = "User with the username does not exist"
            }
            else{
              this.error = err.response.data
            }
            
            this.alert = true;
          })
         ;
      } else {
        this.errorText = "A message must be entered!";
      }
    },

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