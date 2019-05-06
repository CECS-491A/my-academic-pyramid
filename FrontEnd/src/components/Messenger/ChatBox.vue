<template>
  <div class="chat box">
    <v-toolbar dark color="primary darken-1">
      <!-- Hold the username of user talking with  -->
      <h2 class="text-primary text-center">{{contactUsername}}</h2>  
    </v-toolbar>
    <div class="card">
      <div class="card-body">
        <v-card>
          <p class="nomessages text-secondary" v-if="messages.length == 0">[No messages yet!]</p>
          <!-- List of message that scrollable -->
          <v-list v-chat-scroll="{always: false, smooth: true}">
            <div v-for="message in messages" :key="message.CreateDate">
              <!-- Chat usernames -->
              <v-chip
                label
                :color="message.OutgoingMessage == true ? 'gray' : 'primary'"
                :text-color="message.OutgoingMessage == true ? 'black' : 'white'"
              >{{ message.SenderUsername }}</v-chip>
              <!-- Chat messages -->
              <v-chip
                :color="message.OutgoingMessage == true ? 'gray' : 'primary'"
                :text-color="message.OutgoingMessage == true ? 'black' : 'white'"
              >{{message.MessageContent}}</v-chip>
            </div>
          </v-list>
        </v-card>
      </div>
      <!-- Create chat reply box -->
      <div class="card-action">
        <div class="Reply Box" style="margin-bottom: 30px">
          <v-toolbar card color="light-blue" dark>
            <v-toolbar-title>Replying</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-icon large color="blue darken-2">chat</v-icon>
          </v-toolbar>

          <v-form @submit.prevent="SendMessageWithExistingConversation">
            <v-text-field
              id="inputMessage"
              v-model="newMessage.MessageContent"
              label="Message"
              outline
              clearable

            ></v-text-field>
           
            <button id="sendMsgButton" class="btn btn-primary" type="submit" name="action">
              <v-btn color="success">Send</v-btn>
            </button>
            <!-- Show error  -->
            <v-alert :value="error" type="error" transition="scale-transition">{{error}}</v-alert>
          </v-form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { hubConnection } from "signalr-no-jquery"; // import Signal Hub

export default {
  name: "Chat",
  props: ["name"],

  data() {
    return {
      messages: [], // hold return message in a conversation
      newMessage: { // used for compiling a new message
      //conversation id will be passed in from Chat component when user click on the conversation 
        conversationId: "", 
        MessageContent: "" 
      },
      currentConversationId: "", 
      currentContactUsername: "",
      authUserId: "",
      authUsername: "",
      contactUsername: "",

      connection: "", // connection for SignalR hub
      hubProxy: "", //Hold SignalR HubConnection created from the connection above
      error: ""
    };
  },
  watch: {
    //When current conversation id changes, fetch the messages again
    currentConversationId: function() {
      this.getMessageInConversation(this.currentConversationId),
        (this.newMessage.conversationId = this.currentConversationId);
    }
  },
  created() {
    this.getAuthUserIdAndUsername(),
    this.connection = hubConnection(this.$signalRHostName);
    this.hubProxy = this.connection.createHubProxy("MessengerHub");

    // SignalR Hub listener from backend to fetch the message in a conversation with given conversationId
    this.hubProxy.on("FetchMessages", conversationIdToFetch => {
      this.GetRecentMessage(conversationIdToFetch),
      this.$eventBus.$emit("ReloadChatHistoryList");
    });

    // Listen for get message coommand  from chat view when select another conversation
    this.$eventBus.$on("GetMessageInConversation", conversationId => {
      this.currentConversationId = conversationId;
    });

    // Listen to clear that chat screen when the current conversation is deleted
    this.$eventBus.$on("ClearChatScreen", () => {
      this.contactUsername = "";
      this.messages = [];
    });
  },

  methods: {
    // Method to retrieve auth user Id and username at initial
    getAuthUserIdAndUsername() {
      this.axios({
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.SITtoken
        },
        method: "GET",
        crossDomain: true,
        url: this.$hostname + "messenger/GetAuthUserIdAndUsername"
      })
        .then(response => {
          this.authUserId = response.data.authUserId,
          this.authUsername = response.data.authUsername,

          // Attach the auth user id with signalR request to the backend for connection id mapping
          this.connection.qs = "authUserId=" + this.authUserId,
          this.connection
            .start()
            .done(function() {
              console.log("SignalR Hub Now connected");
            })
            .fail(function() {
              console.log("SignalR Hub Could not connect");
            });

          // sessionStorage.SITtoken = response.data.SITtoken
        })
        .catch(err => {
          /* eslint no-console: "off" */
          console.log(err);
        });
    },

    // Get all messages in a conversation
    async getMessageInConversation(conversationId) {
      await this.axios({
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.SITtoken
        },
        method: "GET",
        crossDomain: true,
        url:
          this.$hostname +
          "messenger/GetMessageInConversation?conversationId=" +
          conversationId
      })
        .then(response => {
          this.messages = response.data.messages;
          this.contactUsername = response.data.contactUsername; // Get contact username to display in chat box
          this.$eventBus.$emit("ReloadChatHistoryList");
          // sessionStorage.SITtoken = response.data.SITtoken
        })
        .catch(err => {
          /* eslint no-console: "off" */
          console.log(err);
        });
    },
    async GetRecentMessage(conversationId) {
      await this.axios({
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
          Authorization: "Bearer " + sessionStorage.SITtoken
        },
        method: "GET",
        crossDomain: true,
        url:
          this.$hostname +
          "messenger/GetRecentMessage?conversationId2=" +
          conversationId
      })
        .then(response => {
          if (
            response.data.message.ConversationId == this.currentConversationId
          ) {
            this.messages.push(response.data.message);
          }

          //sessionStorage.SITtoken = response.data.SITtoken
        })
        .catch(err => {
          /* eslint no-console: "off" */
          console.log(err);
        });
    },

    // Method to send a message after a conversation is chosen
    async SendMessageWithExistingConversation() {
      if (this.newMessage.MessageContent) {
        await this.axios({
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
            Authorization: "Bearer " + sessionStorage.SITtoken
          },
          method: "POST",
          crossDomain: true,
          url: this.$hostname + "messenger/SendMessageExistingConversation",
          data: this.newMessage
        })
          .then(response => {
            this.messages.push(response.data.message); // Push the newly creately message to the current shown messages
            //this.GetRecentMessage(this.currentConversationId)
            this.newMessage.MessageContent = null;
            //sessionStorage.SITtoken = response.data.SITtoken
          })

          .catch(err => {
            /* eslint no-console: "off" */
            console.log(err);
          });

        this.error = "";
      } else {
        this.error = "A message must be entered!";
      }
    }
  }
};
</script>

<style>
.v-list {
  height: 300px;
  overflow-y: auto;
}

</style>
