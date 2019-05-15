
<template>

  <v-app>
    <div class="home" v-bind="{backgroundImage:'url(' + require('@/assets/background.jpg') + ')' }">
      <div class="home">
        <h3>Hello {{ username }}</h3>
      </div>
      <v-layout row wrap>
        <v-flex lg9>
          <h1 id="appSelection">All Features</h1>
        </v-flex>
      </v-layout>
            <v-container fluid grid-list-md>
              <v-layout row wrap  >
                <v-flex xs4  v-for="(app, value) in appFeatures" :key="value">
                  <v-card hover flat tile class="mx-auto" width="344">
                    <v-img
                      :aspect-ratio="16/9"
                      :src="getImgUrl(app.LogoUrl)"
                      @click="launch(app.route)"
                    ></v-img>
                    <v-card-title class="teal lighten-4" primary-title>
                      <div>
                        <span class="headline" row-wrap>{{app.name}}</span>
                      </div>
                    </v-card-title>
                  </v-card>
                </v-flex>
              </v-layout>
            </v-container>
            </div>
  </v-app>

</template>
<script>
//import axios from 'axios'
import AppSession from "@/services/AppSession";

export default {
  name: "UserHomePage",
  data() {
    return {
      username: "",
      userid: "",
      appFeatures: [
        {
          value: "discussionForum",
          name: "Discussion Forum",
          route: "/discussionForum",
          LogoUrl: "FeatureLogos/forum.png"
        },
        {
          value: "chat",
          name: "Chat Messenger",
          route: "/chat",
          LogoUrl: "FeatureLogos/chat.png"
        },
        {
          value: "Search",
          name: "Search",
          route: "/search",
          LogoUrl: "FeatureLogos/search.png"
        },
        // {
        //   value: "analysisDashboard",
        //   name: "Analysis Dashboard",
        //   route: "/Dashboard",
        //   LogoUrl: "FeatureLogos/dashboard.png"
        // },
        // {
        //   value: "logging",
        //   name: "Logging",
        //   route: "/Logging",
        //   LogoUrl: "FeatureLogos/logging.png"
        // },
        // {
        //   value: "userManagement",
        //   name: "User Management",
        //   route: "/UserManagement",
        //   LogoUrl: "FeatureLogos/userManagement.png"

        // },
        {
          value: "userProfile",
          name: "User Profile",
          route: "",
          LogoUrl: "FeatureLogos/profile.png"
        },
        {
          value: "schoolRegistration",
          name: "School Registration",
          route: "/SchoolRegistration",
          LogoUrl: "FeatureLogos/SchoolRegistration.png"
        }
      ]
    };
  },

  created() {
    this.userid = AppSession.state.userId;
    // TODO set header
    console.log(`In created of UserHomePage: ${AppSession.state.token}`);
    this.axios
      .get(
        `${this.$hostname}UserManager/GetUserInfoWithId?id=${
          AppSession.state.userId
        }`,
        {
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json",
            Authorization: "Bearer " + AppSession.state.token
          }
        }
      )
      .then(response => {
        this.username = response.data.User.UserName;
        console.log(
          `Updating session in UserHomPage. New token ${response.data.SITtoken}`
        );
        AppSession.updateSession(response.data.SITtoken);
      })
      .catch(error => {
        // display error
      });
  },
  methods: {
    launch(route) {
      this.$router.push(route);
    },

    getImgUrl(pic) {
      return require("../assets/" + pic);
    }
  }
};
</script>
<style>
.home {
  background-image: url("../assets/background.jpg");
  width: 2000px;
  margin: auto;
}
</style>
