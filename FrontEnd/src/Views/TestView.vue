<template> 
<v-app>
  <v-container>
    <ToolBarComponent></ToolBarComponent>

    <!-- <TestComponent></TestComponent> -->
    <PostQuestionDialog></PostQuestionDialog>

    <!-- <ViewQuestions></ViewQuestions> -->

  </v-container> 
 </v-app>  
</template>

<script>
import ToolBarComponent from "@/components/DiscussionForum/ToolBarComponent";
import ViewQuestions from "@/components/DiscussionForum/ViewQuestions";
import PostQuestionDialog from "@/components/DiscussionForum/PostQuestionDialog";

// import TestComponent from "@/components/DiscussionForum/TestComponent";

export default {
  name: "TestView",
  components: {
    ToolBarComponent,
    ViewQuestions,
    PostQuestionDialog
    // TestComponent,
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