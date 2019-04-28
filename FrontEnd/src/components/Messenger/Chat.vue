<template>
  <div class="chat container">
    <v-toolbar dark color="primary darken-1">
      <h2 class="text-primary text-center">{{contactUsername}}</h2>
    </v-toolbar>
    <div class="card">
      <div class="card-body">
        <v-card>
          <p class="nomessages text-secondary" v-if="messages.length == 0">[No messages yet!]</p>
          <div class="messages" v-chat-scroll="{always: false, smooth: true}">
            <div v-for="message in messages" :key="message.CreatedDate">
              <span class="text-info">{{ message.SenderUsername }}]:</span>
              <span>{{message.MessageContent}}</span>
              <span class="text-secondary time">{{message.timestamp}}</span>
            </div>
          </div>
        </v-card>
      </div>
      <div class="card-action">
        <div class="Reply Box" style="margin-bottom: 30px">
          <v-toolbar card color="light-blue" dark>
            <v-toolbar-title>Replying</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-icon large color="blue darken-2">chat</v-icon>
          </v-toolbar>
          <v-form @submit.prevent="SendMessageWithExistingConversation">
            <v-text-field v-model="newMessage.MessageContent" label="Message" outline clearable></v-text-field>
            <p class="text-danger" v-if="errorText">{{ errorText }}</p>
            <button class="btn btn-primary" type="submit" name="action">
              <v-btn color="success">Send</v-btn>
            </button>
          </v-form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import CreateMessage from "@/components/Messenger/CreateMessage";
// import $ from 'jquery'
// window.$ = window.jQuery = require("jquery");
// require('signalr');
import { hubConnection } from "signalr-no-jquery";

export default {
  name: "Chat",
  props: ["name"],

  data() {
    return {
      messages: [],
      newMessage: {
        conversationId: "",
        MessageContent: ""
      },
      currentConversationId: "",
      connection: "",
      hubProxy: "",
      authUsername: "",
      currentContactUsername: "",
      errorText: "",

      authUserId: "",
      authUsername: "",
      contactUsername: ""
    };
  },

  watch: {
    currentConversationId: function() {
      this.getMessageInConversation(this.currentConversationId),
        (this.newMessage.conversationId = this.currentConversationId);
    }
  },
  created() {
    this.getAuthUserIdAndUsername(),
      (this.connection = hubConnection("http://localhost:59364/"));
    
    console.log(sessionStorage.token);
    this.hubProxy = this.connection.createHubProxy("MessengerHub");

    this.hubProxy.on("FetchMessages", conversationIdToFetch => {
      this.GetRecentMessage(conversationIdToFetch),
        this.$eventBus.$emit("ReloadChatHistoryList");
    });

    this.$eventBus.$on("GetMessageInConversation", conversationId => {
      this.currentConversationId = conversationId;
    });

    this.$eventBus.$on("GetRecentMessage", conversationId => {
      this.currentConversationId = conversationId;
      this.GetRecentMessage(this.currentConversationId);
    });

    this.$eventBus.$on("ClearChatScreen", () => {
      this.messages = [];
    });
  },

  methods: {
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
          (this.authUserId = response.data.authUserId),
            (this.authUsername = response.data.authUsername),
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
          this.currentConversationId = conversationId;
          this.contactUsername = response.data.contactUsername;
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
            this.messages.push(response.data.message);
            //this.GetRecentMessage(this.currentConversationId)
            this.newMessage.MessageContent = null;
            //sessionStorage.SITtoken = response.data.SITtoken
          })

          .catch(err => {
            /* eslint no-console: "off" */
            console.log(err);
          });

        this.errorText = null;
      } else {
        this.errorText = "A message must be entered!";
      }
    }
  }
};
</script>

<style>
.chat h2 {
  font-size: 2.6em;
  margin-bottom: 0px;
}

.chat h5 {
  margin-top: 0px;
  margin-bottom: 40px;
}

.chat span {
  font-size: 1.2em;
}

.chat .time {
  display: block;
  font-size: 0.7em;
}

.messages {
  max-height: 300px;
  overflow: auto;
}
</style>