<template>
  <v-navigation-drawer app v-model="navigationBarState.drawerIsOpen">
    <v-layout column align-center>
      <v-flex class="mt-5">
        <v-avatar size="100">
          <img src="">
        </v-avatar>
        <p class="black--text subheading mt-1">Professor Vong</p>
      </v-flex>
    </v-layout>
    <v-list>
      <v-list-tile v-for="link in linksToDisplay" :key="link.name" router :to="link.route">
        <v-list-tile-action>
          <v-icon class="purple--text">{{link.icon}}</v-icon>
        </v-list-tile-action>
        <v-list-tile-content>
          <v-list-tile-title class="purple--text">{{link.text}}</v-list-tile-title>
        </v-list-tile-content>
      </v-list-tile>
      <v-list-tile>
        <v-list-tile-content>
          <v-list-tile-title>
            {{computedTestObj}}
          </v-list-tile-title>
        </v-list-tile-content>
      </v-list-tile>
    </v-list>
  </v-navigation-drawer>
</template>

<script>
//import axios from 'axios'
import AppSession from "@/services/AppSession";
import NavBarState from "@/services/NavBarState"

export default {
  name: "NavBar",
  data() {
    return {
      info: "My Academic Pyramid Homepage",
      navigationBarState: NavBarState.state,
      sessionState: AppSession.state,
      links: [
        // If true, show when logged in
        // If false, show when logged out
        { name: "Home", icon: "home", text: "Home", route: "/" },
        {
          name: "UserHomePage",
          icon: "contacts",
          text: "User HomePage",
          route: "/UserHomePage",
          onlyActive: true
        },
        {
          name: "MyProfile",
          icon: "",
          text: "My Profile",
          // sessionState isn't defined when this is being parsed....
          route: `/Profile/${(this.sessionState != undefined) ? this.sessionState.userId : ""}`,
          onlyActive: true
        },
        {
          name: "Search",
          icon: "search",
          text: "Search",
          route: "/",
          onlyActive: true
        },
        {
          name: "Dashboard",
          icon: "dashboard",
          text: "Dashboard",
          route: "/Dashboard",
          onlyActive: true
        },
        {
          name: "UserManagement",
          icon: "person",
          text: "User Management",
          route: "/UserManagement",
          onlyActive: true
        },
        {
          name: "Chat",
          icon: "chat",
          text: "Chat",
          route: "/Chat",
          onlyActive: true
        },
        {
          name: "Logging",
          icon: "restore",
          text: "Logging",
          route: "/Logging",
          onlyActive: true
        },
        {
          name: "Publish",
          icon: "launch",
          text: "Publish",
          route: "/publish",
          onlyActive: true
        },
        {
          name: "DiscussionForum",
          icon: "forum",
          text: "Discussion Forum",
          route: "/DiscussionForum",
          onlyActive: true
        }
      ], 
      testObj: {greeting: "hello"},
      computedTestObj: "default"
    };
  },
  created() {
    AppSession.synchAppSession();
    console.log(`This is the userId ${this.sessionState.userId}`)
  },
  watch: {
    sessionState : {
      handler: function(newSessionState, oldSessionState) {
        console.log('Session state has been changed.')
        let profileLink = this.links[2]
        // When session changes, update the link to profile.
        profileLink.route = 
          `/Profile/${(newSessionState.userId != undefined) ? newSessionState.userId : ""}`
      },
      deep: true // Vue watches for changes to properties of object.
    },
    testObj : function(newT, oldT) {
      this.computedTestObj = "a" + newT.greeting
    }
  },
  computed: {
    linksToDisplay: function() {
      // sessionState = this.sessionState;
      return this.links.filter(function(link) {
        if (this.sessionState.token) {
          // it's there
          console.log('There is a session.')
          let generateButton = false;
          if (link.onlyActive) {
            generateButton = true;
          }
          return generateButton;
        }
        else {
          console.log('No active session.')
          let generateButton = false;
          if (!link.onlyActive) {
            generateButton = true;
          }
          return generateButton;
        }
      }, this);
    }
  }
};
</script>
