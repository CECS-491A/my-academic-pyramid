<template>
  <div class="chat container">
      <v-toolbar dark color="primary darken-1">
        <h2 class="text-primary text-center">Real-Time Chat</h2>
      </v-toolbar>
      <div class="card">
        <div class="card-body">
          <v-card>
            <p class="nomessages text-secondary" v-if="messages.length == 0">[No messages yet!]</p>
            <div class="messages" v-chat-scroll="{always: false, smooth: true}">
              <div v-for="message in messages" :key="message.id">
                <span class="text-info">[{{ message.ReceiverId }}]:</span>
                <span>{{message.MessageContent}}</span>
                <span class="text-secondary time">{{message.timestamp}}</span>
              </div>
            </div>
          </v-card>
        </div>
        <div class="card-action">
          <CreateMessage :name="name"/>
        </div>
      </div>
  </div>
</template>

<script>
    import CreateMessage from '@/components/Messenger/CreateMessage';
    // import $ from 'jquery'
    // window.$ = window.jQuery = require("jquery");
    // require('signalr');
    import {hubConnection} from 'signalr-no-jquery'


    export default {
        name: 'Chat',
        props: ['name'],
        components: {
            CreateMessage
        },
        data() {
            return{
                messages: [],
                selectedUserId:"",
                connection:"",
                hubProxy:""

            }
        },
        created() {
            this.loadMessage(),
          
          this.$eventBus.$on("LoadMessageContact",receiverId =>{
                this.selectedUserId = receiverId
                this.loadMessage(receiverId)
            });

             this.connection = hubConnection("http://localhost:59364/");
          this.connection.qs = "jwt=2";
           console.log(sessionStorage.token);
           this.hubProxy = this.connection.createHubProxy("MessengerHub");

            this.hubProxy.on('FetchMessages', ()=> {
              this.loadLatestMessage (this.selectedUserId)  
            });

            this.connection.start()
           .done(function(){ console.log('Now connected, connection ID=' + this.connection.id); })
           .fail(function(){ console.log('Could not connect'); });
       

            
        },

       
        methods:{
           async loadMessage(receiverId){
                    await this.axios({
                        method: "GET",
                        crossDomain: true,
						url: this.$hostname + "messenger/LoadMessageContact?receiverUserId=" + receiverId,	
                    })
                    .then(response => {
                        this.messages = response.data;
                    })
                    .catch(err => {
                        /* eslint no-console: "off" */
                        console.log(err);
                    });
                

            },
            async loadLatestMessage(receiverId){
                    await this.axios({
                        method: "GET",
                        crossDomain: true,
						url: this.$hostname + "messenger/LoadLatestMessageContact?receiverUserId2=" + receiverId,	
                    })
                    .then(response => {
                        this.messages.push(response.data);
                    })
                    .catch(err => {
                        /* eslint no-console: "off" */
                        console.log(err);
                    });
                

            }
        }
    }
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