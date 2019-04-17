<template>
<div id = "chatApp">
<v-app id ="chat">
  <v-container fluid grid-list-xl>
    <v-layout row wrap>
      <v-flex xs12 md4>
  <v-card height="350px">
    <v-navigation-drawer
      v-model="drawer"
      permanent
      absolute
    >
      <v-toolbar flat class="transparent">
        <v-list class="pa-0">
          <v-list-tile avatar>
            <v-list-tile-avatar>
              
            </v-list-tile-avatar>

            <v-list-tile-content>
              <v-list-tile-title>Chat APP</v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
           <v-btn
              fab
              small
              color="red"
              bottom
              left
              absolute
              @click="chatDialog = !chatDialog"
            >
              <v-icon>add</v-icon>
            </v-btn>
        </v-list>
      </v-toolbar>

      <v-list class="pt-0" dense>
        <v-divider></v-divider>

        <v-list-tile
          v-for="item in chatHistory"
        
          :key="item.ReceiverUsername"
          @click="loadMessageWithContact(item.ReceiverUserName)"

        >
          <v-list-tile-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-tile-action>

          <v-list-tile-content>
            <v-list-tile-title>{{ item.ReceiverUserName }}</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
  </v-card>
  <friendList></friendList>
      </v-flex>
              <v-flex
          xs12
          md6
        >
        <v-card class="elevation-12">
          <chat> </chat>

           <v-dialog v-model="chatDialog" max-width="500px">
            <v-card>
              <v-card-text>
                <v-text-field 
                label="Recipient Username"
                v-model="conversation.receiverUsername"
                ></v-text-field>
                <v-text-field 
                label="Message"
                v-model="conversation.messageContent"
                ></v-text-field>
                <small class="grey--text">* This doesn't actually save.</small>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn flat color="primary" @click="sendMessageWithNewContact">Submit</v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
          
        </v-card>
              </v-flex>
    </v-layout>
  </v-container>
</v-app>
</div>
</template>

<script>
import chat from "@/components/Messenger/Chat"
import friendList from"@/components/Messenger/FriendList"

    import {hubConnection} from 'signalr-no-jquery'
  export default {
    components:{
      chat,
      friendList
    },
    data () {
      return {
        drawer: true,
        chatHistory: [],
        right: null,
        chatDialog: false,
        conversation:{
					senderUsername: "nguyentrong56@gmail.com",
					receiverUsername: "",
					messageContent:""
        },
 
        errorText: null
        
      }
    },
    created(){
      this.loadContactHistory(),
      this.$eventBus.$on("SendMessageFromFriendList",receiverUsername =>{
        this.conversation.receiverUsername = receiverUsername,
        this.conversation.messageContent=""
        this.chatDialog = true
      })
    },
    methods: {
      async loadContactHistory(){
        await this.axios({
                        method: "GET",
                        crossDomain: true,
						url: this.$hostname + "messenger/GetContactHistory" ,
						
                    })
                    .then(response => {
                        this.chatHistory = response.data;
                    })
                    .catch(err => {
                        /* eslint no-console: "off" */
                        console.log(err);
                    });
      },
    loadMessageWithContact(receiverUsername){
      this.$eventBus.$emit("LoadMessageContact", receiverUsername)
    },

    async sendMessageWithNewContact()
    {
      if (this.conversation.messageContent) {
                    await this.axios({
                       
						method: "POST",
						crossDomain: true,
						url: this.$hostname + "messenger/SendMessage" ,
						data: this.conversation
                    })
                    
					.catch(err => {
                        /* eslint no-console: "off" */
                        console.log(err);
                    });
                    this.conversation.initialMessage = null;
                    this.errorText = null;
                    this.chatDialog = false;
                    this.loadContactHistory();
                    this.$eventBus.$emit("LoadMessageContact", this.conversation.receiverUsername)
                } else {
                    this.errorText = "A message must be entered!"
                }
    }


    }
  

  }
</script>