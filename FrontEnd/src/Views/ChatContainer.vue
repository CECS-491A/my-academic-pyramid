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
      </v-flex>
              <v-flex
          xs12
          md6
        >
        <v-card class="elevation-12" color="primary lighten-2">
          <chat> </chat>
        </v-card>
              </v-flex>
    </v-layout>
  </v-container>
</v-app>
</div>
</template>

<script>
import chat from "@/components/Messenger/Chat"

    import {hubConnection} from 'signalr-no-jquery'
  export default {
    components:{
      chat
    },
    data () {
      return {
        drawer: true,
        chatHistory: [],
        right: null,
        connection:"",
        hubProxy:""
      }
    },
    created(){
      this.loadContactHistory()
       this.connection = hubConnection("http://localhost:59364");
          this.connection.qs = "jwt=nguyentrong56@gmail.com";
           
           this.hubProxy = this.connection.createHubProxy("MessengerHub");

            this.hubProxy.on('FetchMessages', ()=> {
              this.loadMessageWithContact ("Admin@gmail.com")  
            });

           this.connection.start()
           .done(function(){ console.log('Now connected, connection ID=' + this.connection.id); })
           .fail(function(){ console.log('Could not connect'); });
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
    }
    }
  

  }
</script>