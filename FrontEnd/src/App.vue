<template>
  <nav>
    <v-content>
      <keep-alive>
        <router-view/>
      </keep-alive>
    </v-content>
    <v-toolbar flat app>
      <v-toolbar-side-icon class="grey--text" @click="drawer = !drawer"></v-toolbar-side-icon>
      <v-toolbar-title>
        <span>My Academic Pyramid</span>
      </v-toolbar-title>
      <v-spacer></v-spacer>
<v-badge v-model="newMessage" color="red">
        <template v-slot:badge>
          <span>!</span>
        </template>
        <v-icon
         large color="grey"
         @click="goToChatScreen">mail</v-icon>
      </v-badge>
      <v-btn>
        <span>Sign Out</span>
        <v-icon right>exit_to_app</v-icon>
      </v-btn>

      
    </v-toolbar>

    <v-navigation-drawer app v-model="drawer">
      <v-layout column align-center>
        <v-flex class="mt-5">
          <v-avatar size="100">
            <img src=""">
          </v-avatar>
          <p class="black--text subheading mt-1">User</p>
        </v-flex>
      </v-layout>
      <v-list>
        <v-list-tile v-for="link in links" :key="link.name" router :to="link.route">
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
import NavBarComp from "@/components/NavBarComp";
// TODO figure out how to make a toolbar component.
export default {
  name: "App",
  components: {
    NavBarComp,
  },
  data() {
    return {
      drawer: false,
      links: [
        { name: 'Home', icon: 'home', text: 'Home', route: '/'},
        { name: 'Login',icon: 'face', text: 'Login', route: '/login'},
        { name: 'Search', icon: 'search', text: 'Search', route: '/'},
        { name: 'Dashboard', icon: 'dashboard', text: 'Dashboard', route: '/Dashboard'},
        { name: 'UserManagement', icon: 'person', text: 'User Management', route: '/UserManagement'},
        { name: 'Chat', icon: 'chat', text: 'Chat', route: '/Chat'},
        { name: 'Logging', icon: 'restore', text: 'Logging', route: '/Logging'},
        { name: 'Publish', icon: 'launch', text: 'Publish', route: '/publish'},
        { name: 'DiscussionForum', icon: 'forum', text: 'Discussion Forum', route: '/DiscussionForum'},
        { name: 'UserRegistration', icon: 'account_box', text: 'User Registration', route: '/UserRegistration'},
        { name: 'UserHomePage', icon: 'contacts', text: 'User HomePage', route: '/UserHomePage'}
      ],
      newMessage:false
    }
  },
   created() {
    this.$eventBus.$on("ReloadChatHistoryList", () => {
      this.newMessage = true;
    });
  },
  methods: {
    goToChatScreen() {
      this.$router.push("/Chat"), (this.newMessage = false);
    }
  }
};

</script>


}
