<template>
  <v-dialog v-model="forumState.postQuestionForm" max-width="750">
    <!-- <div class="modal">  -->
      <!-- <v-container>     -->
      <v-card dark > 
        <h1>Post Question</h1>

        <v-form>
        <v-textarea
            name="text"
            id="text"
            type="text"
            v-model="text"
            label="Enter question between 50 and 2000 characters"
            auto-grow
            v-if="!validation"
            rows="3"
            /><br />
        <v-text-field
            name="exp"
            id="exp"
            type="exp"
            v-model="exp"
            label="Enter exp a user needs to answer your question" 
            v-if="!validation"
            /><br />
        
          <v-btn id="btnDraft" color="grey" v-if="!validation" v-on:click="postDraft">Save Draft</v-btn>


        
        <v-alert
            :value="error"
            id="error"
            type="error"
            transition="scale-transition"
        >
            {{error}}
        </v-alert>

        <div v-if="validation" id="postQuestion">
            <h3>{{ validation }}</h3>
        </div>

        <v-btn id="btnPostQuestion" color="success" v-if="!validation" v-on:click="postQuestion">Post Question</v-btn>

        <v-btn id="btnClose" color="grey" v-on:click="ClosePostQuestionForm()">Close</v-btn>

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
  name: "PostQuestionDialog",
  data () {
    return {
     forumState: ForumState.state,
      //postQuestionDialog: true,
      validation: null,
      text: '',
      exp: '',
      underMaintenance: false,
      error: '',
      loading: false
    }
  },
  methods: {
    // close() {
    //   this.$emit('close');
    // },
    ClosePostQuestionForm() {
      ForumState.closePostQuestionForm()
    },
    postQuestion: function () {
      this.error = "";
      if (this.text.length < 50 || this.text.length > 2000) {
        this.error = "Invalid text length";
      }

      if (this.exp.length == 0) {
        this.exp = 0;
      }

      if (this.exp < 0) {
        this.error = "Invalid exp number";
      }

      if (this.error) return;

      const url = 'DiscussionForum/PostQuestion'
      this.loading = true;
      axios.post(this.$hostname + url, {
        QuestionType: "SchoolQuestion",
        AccountId: sessionStorage.SITuserId,
        SchoolId: this.forumState.school,
        //SchoolId: "1",        
        DepartmentId: this.forumState.department,
        CourseId: this.forumState.course,
        Text: this.text,
        Exp: this.exp,
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
          ForumState.closePostQuestionForm()
        })
    },
    postDraft: function () {
      this.error = "";
      if (this.text.length > 2000) {
        this.error = "Invalid text length";
      }

      if (this.exp.length == 0) {
        this.exp = 0;
      }

      if (this.exp < 0) {
        this.error = "Invalid exp number";
      }

      if (this.error) return;

      const url = 'DiscussionForum/PostQuestion'
      this.loading = true;
      axios.post(this.$hostname + url, {
         
        //'Send': 'application/json'
          QuestionType: "DraftQuestion",
          AccountId: sessionStorage.SITuserId,
          Text: this.text,
          Exp: this.exp,
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
      })
        .then(response => {
          this.validation = response.data 
        })
        .catch(err => {
          this.error = err.response.data
        })
        .finally(() => {
          this.loading = false;
          ForumState.closePostQuestionForm()
        })
    }
  }
}
</script>
