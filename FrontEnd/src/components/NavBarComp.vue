<template>
  <v-toolbar app>
      <v-toolbar-title class="headline text-uppercase">
      <span class="font-weight-light">My Academic Pyramid</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn to="/" flat>Home</v-btn>
      <v-btn v-if="!sessionState.token" to="Login" flat>Login</v-btn>
      <v-btn icon v-if="sessionState.token" to="Search" flat>
        <v-icon>search</v-icon>
      </v-btn>
      <v-btn v-if="sessionState.token" to="DiscussionForum" flat>DiscussionForum</v-btn>
      <v-btn v-if="sessionState.token" to="Publish" flat>Publish</v-btn>
      <v-btn v-if="sessionState.token" to="Logging" flat>Logging</v-btn>
      <v-btn v-if="sessionState.token" to="UserManagement" flat>UM</v-btn>
      <v-btn v-if="sessionState.token" to="Chat" flat>Chat</v-btn>
      <v-btn v-if="sessionState.token" to="Dashboard" flat>Dashboard</v-btn>
  </v-toolbar>
    
</template>

<script>
//import axios from 'axios'
import AppSession from "@/services/AppSession"

export default {
  name: "NavBar",
  props: ["isOn"],
  data () {
    return {
      info: "My Academic Pyramid Homepage",
      sessionState: AppSession.state
    }
  },
  created() {
    console.log("In created of NavBar")
    AppSession.synchAppSession()
  }
  
}
</script>