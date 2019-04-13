<template>
    <div class="publish-wrapper">
        
        <h1>Publish Your Application</h1>

        <br />
        <v-form>
        <v-text-field
            name="key"
            id="key"
            v-model="key"
            type="key"
            label="API Key" 
            v-if="!validation"
            /><br />
        <v-text-field
            name="title"
            id="title"
            v-model="title"
            type="title"
            label="Application Title" 
            v-if="!validation"
            /><br />
        <v-textarea
            name="description"
            id="description"
            type="description"
            v-model="description"
            label="Description"
            auto-grow
            v-if="!validation"
            rows="2"
            /><br />
        <v-text-field
            name="logoUrl"
            id="logoUrl"
            type="logoUrl"
            v-model="logoUrl"
            label="Logo Url" 
            v-if="!validation"
            /><br />
        <v-switch 
            id="underMaintenance"
            v-model="underMaintenance"
            :value=underMaintenance
            label="Application Under Maintenance"
            v-if="!validation">
        </v-switch>

        
        <v-alert
            :value="error"
            id="error"
            type="error"
            transition="scale-transition"
        >
            {{error}}
        </v-alert>

        <div v-if="validation" id="publishMessage">
            <h3>Successful Publish!</h3>
            <p>{{ validation }}</p>
        </div>

        <br />

        <v-btn id="btnPublish" color="success" v-if="!validation" v-on:click="publish">Publish</v-btn>

        </v-form>
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
      logoUrl: '',
      underMaintenance: false,
      error: ''
    }
  },
  methods: {
    publish: function () {
      this.error = "";
      if (this.key.length == 0 || this.title.length == 0 || this.description.length == 0 || this.logoUrl.length == 0) {
        this.error = "Fields Cannot Be Left Blank.";
      }

      if (this.error) return;

      const url = 'https://api.kfc-sso.com/api/applications/publish'
      axios.post(url, {
        key: document.getElementById('key').value,
        title: document.getElementById('title').value,
        description: document.getElementById('description').value,
        logoUrl: document.getElementById('logoUrl').value,
        underMaintenance: document.getElementById('underMaintenance').value,
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        }
      })
        .then(response => {
          this.validation = response.data // Retrieve validation message from response
        })
        .catch(err => {
          this.error = err.response.data
        })
    }
  }
}

</script>

<style lang="css">
.publish-wrapper {
    width: 70%;
    margin-top: 20px;
    margin: 1px auto;
}

</style>
