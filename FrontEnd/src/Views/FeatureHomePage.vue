<template>
<v-app >
	<v-parallax
      dark
      :src="require('@/assets/background.jpg')"
    >
    <v-layout row wrap>
      <v-flex lg9>
        <h1 id="appSelection">All Features</h1>
      </v-flex>
      <v-layout row wrap>
        <v-flex xs12 sm6 offset-sm3>
          <v-container fluid grid-list-md>
            <v-layout row-wrap>
              <v-flex md12 sm12 lg6  v-for="(app, value) in appFeatures" :key="value">
                <v-card hover flat tile class="mx-auto" width="344">
                  
                  <v-img
                    :aspect-ratio="16/9"
                    :src="getImgUrl(app.LogoUrl)"
                    @click="launch(app.route)"
                  >
                   
                  </v-img>
				   <v-card-title class="teal lighten-4" primary-title>
					  <div>
                    <span class="headline" row-wrap>{{app.name}}</span>
					  </div>
                  </v-card-title>
                </v-card>
              </v-flex>
            </v-layout>
          </v-container>
        </v-flex>
      </v-layout>
    </v-layout>
	</v-parallax>
</v-app>
</template>
<script>
export default {
  data() {
    return {
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
        }
      ]
    };
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
