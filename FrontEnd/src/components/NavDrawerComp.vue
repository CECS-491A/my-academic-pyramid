<template>
  <v-navigation-drawer 
  class="blue lighten-3"
      dark
     app v-model="navigationBarState.drawerIsOpen">
     <v-img :aspect-ratio="16/9" src="https://cdn.vuetifyjs.com/images/parallax/material.jpg">
    <v-layout column align-center>
      <v-flex class="mt-5">
        <v-avatar size="100">
          <img src="@/assets/Racoon.png">
        </v-avatar>
        <div class="subheading">{{this.$authUsername}}</div>
      </v-flex>
    </v-layout>
     </v-img>
    <v-list>
      <v-list-tile v-for="link in linksToDisplay" :key="link.name" router :to="link.route">
        <v-list-tile-action>
          <v-icon >{{link.icon}}</v-icon>
        </v-list-tile-action>
        <v-list-tile-content>
          <v-list-tile-title >{{link.text}}</v-list-tile-title>
        </v-list-tile-content>
      </v-list-tile>
    </v-list>
  </v-navigation-drawer>

</template>

<script>
//import axios from 'axios'
import AppSession from "@/services/AppSession";
import NavBarState from "@/services/NavBarState"
import AllowedFeaturesService from "@/services/AllowedFeaturesService"
import * as NavLinkNames from "@/constants/NavLinkNames"

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
          name: NavLinkNames.USERHOMEPAGE,
          icon: "contacts",
          text: "User HomePage",
          route: "/UserHomePage",
          onlyActive: true
        },
        {
          name: NavLinkNames.PROFILE,
          icon: "",
          text: "My Profile",
          // sessionState isn't defined when this is being parsed....
          route: `/Profile/${(this.sessionState != undefined) ? this.sessionState.userId : ""}`,
          onlyActive: true
        },
        {
          name: NavLinkNames.SEARCH,
          icon: "search",
          text: "Search",
          route: "/Search",
          onlyActive: true
        },
        {
          name: NavLinkNames.DASHBOARD,
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
          name: NavLinkNames.CHAT,
          icon: "chat",
          text: "Chat",
          route: "/Chat",
          onlyActive: true
        },
        {
          name: NavLinkNames.LOGGING,
          icon: "restore",
          text: "Logging",
          route: "/Logging",
          onlyActive: true
        },
        {
          name: NavLinkNames.PUBLISH,
          icon: "launch",
          text: "Publish",
          route: "/publish",
          onlyActive: true
        },
        {
          name: NavLinkNames.DISCUSSIONFORUM,
          icon: "forum",
          text: "Discussion Forum",
          route: "/DiscussionForum",
          onlyActive: true
        }
      ]
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
    }
  },
  computed: {
    linksToDisplay: function() {
      return this.links.filter(function(link) {
        if (this.sessionState.token) {
          // it's there
          console.log('There is a session.')
          let generateButton = false;
          if (link.onlyActive && this.sessionState.category != undefined
              && AllowedFeaturesService[this.sessionState.category].includes(link.name)) {
            // if (link.name == )
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
