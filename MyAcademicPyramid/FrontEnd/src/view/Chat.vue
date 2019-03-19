
<template>
  <div class='chat container'>
    <h2 class='text-primary text-center'>Real-Time Chat</h2>
    <div class='card'>
      <div class='card-body'>
        <p class='nomessages text-secondary' v-if='messages.length == 0'>[No messages yet!]</p>
        <div class='messages' v-chat-scroll='{always: false, smooth: true}'>
          <div v-for='message in messages' :key='message.Id'>
            <span class='text-info'>[{{ message.Id }}]:</span>
            <span>{{message.MessageContent}}</span>
            <span class='text-secondary time'>{{message.CreatedDate}}</span>
          </div>
        </div>
      </div>
      <div class='card-action'>
        <CreateMessage :name='name'/>
      </div>
    </div>
  </div>
</template>

<script>
/* eslint-disable */
import CreateMessage from '@/components/CreateMessage'
import axios from 'axios'
const signalR = require('signalr');
export default {
  name: 'Chat',
  components: {
    CreateMessage
  },
  props: ['name'],
  data () {
    return {
      messages: null,
      connection: ''
    }
  },
    created: function()
  {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:60500/MessengerHub')
        .configureLogging(signalR.LogLevel.Information)
        .build();
      
        
  },
  mounted: function () {
    var thisVue = this;
    this.connection.start().catch(function(err) {
        return console.error(err.toString());
      });
    thisVue.connection.on('FetchNewMessage', function () {
    axios
      .get('http://localhost:60500/api/Messenger/username', {
        headers: {
          'Content-Type': 'application/json',
          Authorization: 'nguyentrong56'
        },
        params: {
          ReceiverUserName: 'Luis20109'
        }
      })
      .then(response => (this.messages = response.data))
    })

  },




  
}
</script>
