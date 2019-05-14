<template>
  <v-toolbar flat app>
      <v-toolbar-side-icon class="grey--text" @click="flipDrawer"></v-toolbar-side-icon>
      <v-toolbar-title>
        <span>My Academic Pyramid</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-badge v-if="sessionState.userId" v-model="navigationBarState.newMessage" color="red">
        <template v-slot:badge>
          <span>!</span>
        </template>
        <v-icon
         large color="grey"
         @click="goToChatScreen">mail</v-icon>
      </v-badge>
      <v-btn v-if="sessionState.token" @click="logOut">
        <span>Sign Out</span>
        <v-icon right>exit_to_app</v-icon>
      </v-btn>
      <v-btn v-if="!sessionState.token" @click="goToLogin">
        <span>Login</span>
      </v-btn>


    </v-toolbar>
    
</template>

<script>
//import axios from 'axios'
import AppSession from "@/services/AppSession"
import NavBarState from "@/services/NavBarState"

export default {
  name: "NavBar",
  data () {
    return {
      info: "My Academic Pyramid Homepage",
      navigationBarState: NavBarState.state,
      sessionState: AppSession.state,
    }
  },
  methods: {
    logOut() {
      let logoutUrl = `${this.$hostname}logout`
      let headersObject = {
          headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          'Authorization': 'Bearer ' + AppSession.state.token
          }
      }
      let logoutRequest = parseInt(this.sessionState.userId)
      
      // TODO test this
      this.axios.post(logoutUrl, logoutRequest, headersObject)
                .then((response) => {
                  AppSession.logout()
                  console.log("logging off.")
                  this.$router.push("/")
                })
                .catch(error => {

                })
      
    },
    goToLogin() {
      window.location.href = this.$ssoLoginPage
    },

    goToChatScreen() {
      this.$router.push("/Chat")
      NavBarState.messageIsRead()
    },

    flipDrawer() {
      console.log("about to flip drawer.")
      NavBarState.swapDrawer()
    }

  },
  created() {
    AppSession.synchAppSession()
  }
  
}
</script>