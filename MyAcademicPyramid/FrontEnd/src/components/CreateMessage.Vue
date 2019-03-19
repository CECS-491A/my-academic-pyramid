<template>
  <div class='container' style='margin-bottom: 30px'>
    <form @submit.prevent='createMessage'>
      <div class='form-group'>
        <input
          type='text'
          name='message'
          class='form-control'
          placeholder='Enter message ...'
          v-model='newMessage'
        >
        <p class='text-danger' v-if='errorText'>{{ errorText }}</p>
      </div>

      <button class='btn btn-primary' type='submit' name='action'>Submit</button>
    </form>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  name: 'CreateMessage',
  props: ['name'],
  data () {
    return {
      newMessage: null,
      errorText: null
    }
  },
  methods: {
    createMessage () {
      if (this.newMessage) {
        axios.post('http://localhost:60500/api/Messenger/', {
          MessageContent: this.newMessage,
          ReceiverUserName: 'Luis20109'
        })
          .then(response => {})
          .catch(e => {
            this.errors.push(e)
          })
      } else {
        this.errorText = 'A message must be entered'
      }
    }
  }
}
</script>
