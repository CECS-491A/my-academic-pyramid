<template> 
<v-app>
  <v-container>
    <ToolBarComponent></ToolBarComponent>

    <!-- <TestComponent></TestComponent> -->
    <ViewQuestions></ViewQuestions>
    <PostQuestionDialog></PostQuestionDialog>
    <PostAnswerDialog></PostAnswerDialog>
    <EditQuestionDialog></EditQuestionDialog>


  </v-container> 
 </v-app>  
</template>

<script>
import ToolBarComponent from "@/components/DiscussionForum/ToolBarComponent";
import ViewQuestions from "@/components/DiscussionForum/ViewQuestions";
import PostQuestionDialog from "@/components/DiscussionForum/PostQuestionDialog";
import PostAnswerDialog from "@/components/DiscussionForum/PostAnswerDialog";
import EditQuestionDialog from "@/components/DiscussionForum/EditQuestionDialog";
// import TestComponent from "@/components/DiscussionForum/TestComponent";

export default {
  name: "TestView",
  components: {
    ToolBarComponent,
    ViewQuestions,
    PostQuestionDialog,
    PostAnswerDialog,
    EditQuestionDialog,
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