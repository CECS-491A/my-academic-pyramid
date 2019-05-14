<template>
  <v-dialog v-model="postQuestionDialog" max-width="750">
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

        <v-btn id="btnClose" color="grey" v-on:click="postQuestion">Close</v-btn>

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

export default {
  name: "PostQuestionDialog",
  data () {
    return {
      postQuestionDialog: true,
      validation: null,
      text: '',
      exp: '',
      underMaintenance: false,
      error: '',
      loading: false
    }
  },
  methods: {
    close() {
      this.$emit('close');
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
      this.axios.post(this.$hostname + url, {
        QuestionType: "SchoolQuestion",
        AccountId: sessionStorage.SITuserId,
        // SchoolId: getSchoolId(),
        SchoolId: "1",        
        //DepartmentId: getDepartmentId(),
        //CourseId: getCourseId(),
        Text: document.getElementById('text').value,
        Exp: document.getElementById('exp').value.toString(),
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
          this.postQuestionDialag = false;
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
      this.axios.post(this.$hostname + url, {
         
        //'Send': 'application/json'
          QuestionType: "DraftQuestion",
          AccountId: sessionStorage.SITuserId,
          Text: document.getElementById('text').value,
          Exp: document.getElementById('exp').value.toString(),
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
          this.postQuestionDialog = false;
        })
    }
  }
}
</script>
