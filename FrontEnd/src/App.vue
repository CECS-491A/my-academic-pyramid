<template>
  <nav>
    <v-content>
      <keep-alive>
      <router-view />
      </keep-alive> 
    </v-content>
    <v-toolbar flat app>
      <v-toolbar-side-icon class="grey--text" @click="drawer = !drawer"></v-toolbar-side-icon>
      <v-toolbar-title>
        <span>My Academic Pyramid</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn>
        <span>Sign Out</span>
        <v-icon right>exit_to_app</v-icon>
      </v-btn>
    </v-toolbar>
  
    <v-navigation-drawer app v-model="drawer">
      <v-layout column align-center>
        <v-flex class="mt-5">
          <v-avatar size="100">
            <img src="./assets/Vong.jpg">
          </v-avatar>
          <p class="purple--text subheading mt-1">Professor Vong</p>
        </v-flex>
      </v-layout>
      <v-list>
        <v-list-tile v-for="link in links" :key="link.text" router :to="link.route">
          <v-list-tile-action>
            <v-icon class="purple--text">{{link.icon}}</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title class="purple--text">{{link.text}}</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
  </nav>
</template>

<script>
import NavBarComp from "@/components/NavBarComp"
// TODO figure out how to make a toolbar component.
export default {
  name: 'App',
  components: {
    NavBarComp
  },
  data() {
    return {
      drawer: false,
      links: [
        { icon: 'home', text: 'Home', route: '/'},
        { icon: 'face', text: 'Login', route: '/login'},
        { icon: 'search', text: 'Search', route: '/'},
        { icon: 'dashboard', text: 'Dashboard', route: '/Dashboard'},
        { icon: 'person', text: 'User Management', route: '/UserManagement'},
        { icon: 'chat', text: 'Chat', route: '/Chat'},
        { icon: 'restore', text: 'Logging', route: '/Logging'},
        { icon: 'launch', text: 'Publish', route: '/publish'},
        { icon: 'forum', text: 'Discussion Forum', route: '/DiscussionForum'},
        { icon: 'account_box', text: 'User Registration', route: '/UserRegistration'},
        { icon: 'contacts', text: 'User HomePage', route: '/UserHomePage'}
      ]
    }
  }
}
</script>
