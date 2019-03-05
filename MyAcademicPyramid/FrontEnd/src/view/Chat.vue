
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
import CreateMessage from '@/components/CreateMessage'
import axios from 'axios'
export default {
  name: 'Chat',
  components: {
    CreateMessage
  },
  props: ['name'],
  data () {
    return {
      messages: null
    }
  },
  mounted () {
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
  }
}
</script>
