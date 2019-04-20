<template>
<div id = "chatApp">
<v-app id ="chat">
  <v-container fluid grid-list-xl>
    <v-layout row wrap>
      <v-flex xs12 md4>
        <v-tabs
        left
        color="cyan"
        dark
        icons-and-text=""
        >
        <v-tabs-slider color="green"></v-tabs-slider>
        <v-tab id ="chatHistory">
             Chat History
        </v-tab>

        <v-tab-item>
            <v-card height="350px">
  
      <v-toolbar 
      color="teal" dark>
        <v-list class="pa-0">
          <v-list-tile avatar>
            <v-list-tile-avatar>
              
            </v-list-tile-avatar>

            <v-list-tile-content>
              <v-list-tile-title>Chat Messenger</v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
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

      <v-list two-line="">
        <v-divider></v-divider>
        
        <v-list-tile
          v-for="item in chatHistory"
          :key="item.ReceiverId"
          @click="loadMessageWithContact(item.ReceiverId)"

        >
          <v-list-tile-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-tile-action>

          <v-list-tile-content>
            <v-list-tile-title>{{ item.ReceiverUsername }}</v-list-tile-title>
            <v-list-tile-sub-title> {{item.ContactTime}} </v-list-tile-sub-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
  </v-card>

        </v-tab-item>

        <v-tab
        id ="friendList">
        Friend List

        </v-tab>

        <v-tab-item>
  <friendList></friendList>
        </v-tab-item>
        </v-tabs>

      </v-flex>
              <v-flex
          xs12
          md6
        >
        <v-card class="elevation-12">
          <chat> </chat>    
        </v-card>
              </v-flex>
    </v-layout>
  </v-container>
    <v-dialog v-model="chatDialog" max-width="500px">
            <v-card>
              <v-card-text>
                <v-text-field 
                label="Recipient Username"
                v-model="currentReceiverUsername"
                ></v-text-field>
                <v-text-field 
                label="Message"
                v-model="conversation.messageContent"
                ></v-text-field>
                <v-alert
                :value="alert"
                  type="error"
                 transition="scale-transition">
                 {{errorText}}
                  
                </v-alert>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn flat color="primary" @click="sendMessageWithNewContact">Submit</v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
</v-app>
</div>
</template>

<script>
import chat from "@/components/Messenger/Chat"
import friendList from"@/components/Messenger/FriendList"

    //import {hubConnection} from 'signalr-no-jquery'
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
          senderId: 2,
          receiverId:null,
					messageContent:""
        },

        currentReceiverUsername:"null",

        alert: false,
        errorText: null,
        errorFoundUser: null
        
      }
    },
    created(){
      this.loadContactHistory(),
      this.$eventBus.$on("SendMessageFromFriendList",friendTo =>{
      
        this.currentReceiverUsername = friendTo.username,
        this.conversation.receiverId = friendTo.Id,
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
    loadMessageWithContact(receiverId){
      this.$eventBus.$emit("LoadMessageContact", receiverId)
    },

    async sendMessageWithNewContact()
    {
      if (this.conversation.messageContent) {
                    await this.axios({
                       
						method: "POST",
						crossDomain: true,
						url: this.$hostname + "messenger/SendMessage" ,
						data: this.conversation
                    }).then(()=>{

                    this.conversation.initialMessage = null;
                    this.errorText = null;
                    this.chatDialog = false;
                    this.loadContactHistory();
                    this.$eventBus.$emit("LoadMessageContact", this.conversation.receiverId)

                    })        
					.catch(err => {
                        /* eslint no-console: "off" */
                        console.log(err);
                        this.errorFoundUser = err.data;
                        alert = true;

                    });
                   
                } else {
                    this.errorText = "A message must be entered!"
                }
    }


    }
  

  }
</script>