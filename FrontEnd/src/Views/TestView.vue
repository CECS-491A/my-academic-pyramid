<template> 
  <v-container>
    <ToolBarComponent></ToolBarComponent>
    <TestComponent>


    </TestComponent>
  </v-container>  
</template>

<script>
import ToolBarComponent from "@/components/DiscussionForum/ToolBarComponent";
import TestComponent from "@/components/DiscussionForum/TestComponent";

export default {
  name: "TestView",
  components: {
    ToolBarComponent,
    TestComponent,
  },
//   data() {
//     return {
//       drawer: false,
//       newMessage:false
//     }
//   },
//    created() {
//     this.$eventBus.$on("ReloadChatHistoryList", () => {
//       NavBarState.aNewMessageExists();
//     });
//  }
};
</script>