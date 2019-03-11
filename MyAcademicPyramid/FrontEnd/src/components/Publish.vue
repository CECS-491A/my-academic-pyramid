<template>
    <div class="publish-wrapper">
        <form class="form-publish" id="publish" @submit.prevent="publish">
            <h2 class="form-publish-heading">Publish Your Application</h2>
            <input v-model="key" id ="key" class="form-control" v-if="!validation" placeholder="Key" required autofocus>
            <input v-model="title" id="title" class="form-control" v-if="!validation" placeholder="Title" required>
            <textarea v-model="description" id="description" class="form-control" v-if="!validation" placeholder="Description" form="publish" rows="5" required></textarea>
            <input v-model="logoUrl" id="logoUrl" type="logoUrl" class="form-control" v-if="!validation" placeholder="Logo Url" required>
            <button class="button-publish" type="submit" v-if="!validation">Publish</button>
        </form>
        <div v-if="validation" id="hide">
            <h3>Successful Publish!</h3>
        </div>
        <p>{{ validation }}</p>
    </div>
</template>

<script>
import axios from 'axios'

export default {
  data () {
    return {
      validation: null,
      key: '',
      title: '',
      description: '',
      logoUrl: ''
    }
  },
  methods: {
    publish: function () {
      // TODO: replace with SSO backend url
      const url = 'http://localhost:60461/api/applications/publish'
      axios.post(url, {
        key: document.getElementById('key').value,
        title: document.getElementById('title').value,
        description: document.getElementById('description').value,
        logoUrl: document.getElementById('logoUrl').value,
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        }
      })
        .then(response => {
          this.validation = response.data // Retrieve validation message from response
        })
        .catch(function (error) {
          console.log(error)
        })
    }
  }
}

</script>

<style lang="css">
.publish-wrapper {
    background: #fff;
    width: 70%;
    margin: 1px auto;
    text-align: center;
}

.form-publish {
    max-width: 330px;
    padding: 5% 10px;
    margin: 0 auto;
}

.form-publish .form-control {
    position: relative;
    height: auto;
    -webkit-box-sizing: border-box;
        box-sizing: border-box;
    padding: 10px;
    font-size: 16px;
}

.form-publish .form-control:focus {
    z-index: 2;
}

.form-control {
    width: 100%;
    margin-bottom: 10px;
}

.form-publish button {
    height: 40px;
    width: 100%;
}

.form-publish h3 {
    margin-top: 50px;
}

.form-publish textarea {
    resize: none;
}

</style>
