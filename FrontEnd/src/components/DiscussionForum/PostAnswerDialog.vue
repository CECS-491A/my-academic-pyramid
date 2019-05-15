<template>
  <v-dialog v-model="forumState.postAnswerForm" max-width="750">
    <!-- <div class="modal">  -->
      <!-- <v-container>     -->
      <v-card dark > 
        <h1>Post Answer</h1>

        <v-form>
        <v-textarea
            name="text"
            id="text"
            type="text"
            v-model="text"
            label="Enter answer"
            auto-grow
            v-if="!validation"
            rows="3"
            /><br />

        <v-btn id="btnPostAnswer" color="success" v-if="!validation" v-on:click="postAnswer">Post Question</v-btn>

        <v-btn id="btnClose" color="grey" v-on:click="ClosePostAnswerForm()">Close</v-btn>

        </v-form>

        <v-dialog
          v-model="loading"
          hide-overlay
          persistent
          width="300"
        >
          <v-card
            color="primary"
            dark
          >
            <v-card-text>
              Loading
              <v-progress-linear
                indeterminate
                color="white"
                class="mb-0"
              ></v-progress-linear>
            </v-card-text>
          </v-card>
        </v-dialog>
      </v-card> 
      <!-- </v-container>  -->
    <!-- </div> -->
  </v-dialog>
</template>

<script>
import axios from 'axios'
import ForumState from "@/services/ForumState";

export default {
  name: "PostAnwerDialog",
  data () {
    return {
     forumState: this.ForumState.state,
      validation: null,
      text: '',
      exp: '',
      error: '',
      loading: false
    }
  },
  methods: {
    // close() {
    //   this.$emit('close');
    // },
    ClosePostAnswerForm() {
      this.ForumState.closePostAnswerForm()
    },
    postAnswer: function () {
      this.error = "";

      const url = 'DiscussionForum/PostAnswer'
      this.loading = true;
      this.axios.post(this.$hostname + url, {
        QuestionId: this.forumState.question.QuestionId,
        Text: document.getElementById('text').value,
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        }
      })
        .then(response => {
          this.validation = response.data
        })
        .catch(err => {
          this.error = err.response.data
        })
        .finally(() => {
          this.loading = false;
          this.ForumState.closePostAnswerForm()
        })
    },
  }
}
</script>
