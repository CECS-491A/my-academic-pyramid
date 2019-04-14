<template>
  <div class="chat container">
    <v-card class="elevation-12" color="primary lighten-4">
      <v-toolbar dark color="primary darken-1">
        <h2 class="text-primary text-center">Real-Time Chat</h2>
      </v-toolbar>
      <div class="card">
        <div class="card-body">
          <v-card>
            <p class="nomessages text-secondary" v-if="messages.length == 0">[No messages yet!]</p>
            <div class="messages" v-chat-scroll="{always: false, smooth: true}">
              <div v-for="message in messages" :key="message.id">
                <span class="text-info">[{{ message.ReceiverUserName }}]:</span>
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
    </v-card>
  </div>
</template>

<script>
    import CreateMessage from '@/components/Messenger/CreateMessage';
    import $ from 'jquery'
    window.$ = window.jQuery = require("jquery");
    require('ms-signalr-client');
    

    export default {
        name: 'Chat',
        props: ['name'],
        components: {
            CreateMessage
        },
        data() {
            return{
                messages: [],
                selectedUsername:"",
                connection:"",
                hubProxy:""

            }
        },
        created() {
            this.loadMessage(),
            
            this.connection = $.hubConnection("http://localhost:8084/signalr");
            $.connection.logging = true;
            this.hubProxy = this.connection.createHubProxy("MessengerHub"),
            
            this.hubProxy.on('FetchMessages', function(message){
                this.messages.push(message)
            })

            
            this.connection.$hostname
            this.$eventBus.$on("LoadMessageContact",receiverUsername =>{
                this.selectedUsername = receiverUsername
            })
        },
        // mounted(){
        //      this.connection.start().catch(function(err){
        //         return console.error(err)
        //      })
        //     this.hubProxy.on('SendMessage', function(message){
        //         console.log(message)
        //     })

        // },
       
        methods:{
           async loadMessage(receiverUsername){
                    await this.axios({
                        method: "GET",
                        crossDomain: true,
						url: this.$hostname + "messenger?receiverUsername=" + receiverUsername,	
                    })
                    .then(response => {
                        this.messages = response.data;
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