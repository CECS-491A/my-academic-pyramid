<template>
  <nav>
    <v-content>
      <keep-alive>
        <router-view/>
      </keep-alive>
    </v-content>
    <!-- <v-toolbar flat app>
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
      <v-btn >
        <span>Sign Out</span>
        <v-icon right>exit_to_app</v-icon>
      </v-btn>

      
    </v-toolbar> -->
    <NavBarComp />

    <NavDrawerComp ></NavDrawerComp>
  </nav>
</template>

<script>
import NavBarComp from "@/components/NavBarComp";
import NavDrawerComp from "@/components/NavDrawerComp";
import NavBarState from "@/services/NavBarState"
// TODO figure out how to make a toolbar component.
export default {
  name: "App",
  components: {
    NavBarComp,
    NavDrawerComp
  },
  data() {
    return {
      drawer: false,
      
      newMessage:false
    }
  },
   created() {
    this.$eventBus.$on("ReloadChatHistoryList", () => {
      NavBarState.aNewMessageExists();
    });
  }
};

</script>


}
