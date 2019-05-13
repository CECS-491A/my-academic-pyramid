<template>
  <v-app>
    <NavBarComp/>
    <NavDrawerComp></NavDrawerComp>

    <v-content>
      <keep-alive>
        <router-view/>
      </keep-alive>
    </v-content>
  </v-app>
</template>

<script>
import NavBarComp from "@/components/NavBarComp";
import NavDrawerComp from "@/components/NavDrawerComp";
import NavBarState from "@/services/NavBarState";
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
      newMessage: false
    };
  },
  created() {
    this.$eventBus.$on("ReloadChatHistoryList", () => {
      NavBarState.aNewMessageExists();
    });
  }
};
</script>