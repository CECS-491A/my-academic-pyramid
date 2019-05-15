<template>
<v-container>
<v-container v-if="forumState.postedQuestion">
  <v-card class="mx-auto" dark max-width="1400" max-height="800" v-for="item in questions" :key="item.AccountName">

  <!-- <v-card class="mx-auto" dark max-width="1400" max-height="800"> -->

    <v-card-title>{{item.AccountName}}
      <v-spacer></v-spacer>
      {{item.DateCreated}}
    </v-card-title>
    
    <v-card-text class="headline font-weight-bold"> {{item.Text}} </v-card-text>

    <v-card-actions>   
      <v-layout align-center>
        <v-btn small v-if="userId != item.AccoutId" @click="increaseQuestionSpamCount(item.QuestionId)">Mark As Spam</v-btn> {{"Spam Count: " + item.SpamCount}}
        <v-btn small @click="viewAnswers(item)">See Answers</v-btn> {{"Answers: " + item.AnswerCount}}
        <v-btn small v-if="userId != item.AccountId" @click="postAnswer(item)">Post Answer</v-btn> {{"Exp needed to answer: " + item.ExpNeededToAnswer}}

        <v-spacer></v-spacer>

        <v-btn small v-if="userId == item.AccountId" @click="closeQuestion(item.QuestionId)">Close Question</v-btn> 
        <v-icon small v-if="userId == item.AccountId" @click="editQuestion(item.QuestionId)">edit</v-icon>
        <!-- not in the business rules to delete a question 
            <v-icon small v-if="userId == item.AccountId" @click="deleteQuestion()">delete</v-icon> -->

      </v-layout>
    </v-card-actions>

    <v-card-footer>{{ item.SchoolName + "/" + item.DepartmentName + "/" + item.CourseName }}</v-card-footer>

    <v-divider class ="ma-3"></v-divider>
    
  </v-card>
</v-container>

<v-container v-if="!forumState.postedQuestion">
<v-card class="mx-auto" dark max-width="1400" max-height="800" v-for="item in questions" :key="item.AccountName">

  <!-- <v-card class="mx-auto" dark max-width="1400" max-height="800"> -->

    <v-card-title>{{item.AccountName}}
      <v-spacer></v-spacer>
      {{item.DateCreated}}
    </v-card-title>
    
    <v-card-text class="headline font-weight-bold"> {{item.Text}} </v-card-text>

    <v-card-actions>   
      <v-layout align-center>

        <v-spacer></v-spacer>

        <v-icon small v-if="userId == item.AccountId" @click="editQuestion(item.QuestionId)">edit</v-icon>

      </v-layout>
    </v-card-actions>

    <v-divider class ="ma-3"></v-divider>
    
  </v-card>
  </v-container>

<v-container v-if="forumState.viewAnswers">
  <v-card class="mx-auto" dark color="pink" max-width="1400" max-height="800">

    <v-card-title>{{forumState.question.AccountName}}
      <v-spacer></v-spacer>
      {{forumState.question.DateCreated}}
    </v-card-title>
    
    <v-card-text class="headline font-weight-bold"> {{forumState.question.Text}} </v-card-text>

    <v-card-footer>{{ forumState.question.SchoolName + "/" + forumState.question.DepartmentName + "/" + forumState.question.CourseName }}</v-card-footer>
    
  </v-card>
  <v-card class="mx-auto" dark max-width="1400" max-height="800" v-for="item in answers" :key="item.AccountName">

  <!-- <v-card class="mx-auto" dark max-width="1400" max-height="800"> -->

    <v-card-title>{{item.AccountName}}
      <v-spacer></v-spacer>
      {{item.CreatedDate}}
    </v-card-title>
    
    <v-card-text class="headline font-weight-bold"> {{item.Text}} </v-card-text>

    <v-card-actions>   
      <v-layout align-center>
        <v-btn small v-if="userId != item.AccoutId" @click="increaseAnswerSpamCount(item.AnswerId)">Mark As Spam</v-btn> {{"Spam Count: " + item.SpamCount}}
        <v-icon small v-if="userId != item.AccountId" @click="icreaseAnswerHelpfulCount(itemAnswerId)">thumb_up</v-icon> {{"Helpful: " + item.HelpfulCount}}
        <v-icon small v-if="userId != item.AccountId" @click="icreaseAnswerUnHelpfulCount(itemAnswerId)">thumb_down</v-icon> {{"UnHelpful: " + item.UnHelpfulCount}}

        <v-spacer></v-spacer>

        <v-btn small v-if="userId == forumState.question.AccountId" @click="markAsCorrectAnswer(item.AnswerId)">Mark as Correct</v-btn> 

      </v-layout>
    </v-card-actions>

    <v-card-footer>{{ "Is Correct Ans: " + item.IsCorrectAnswer }}</v-card-footer>

    <v-divider class ="ma-3"></v-divider>
    
  </v-card>
</v-container>
</v-container>
</template>


<script>
import axios from 'axios'
import ForumState from "@/services/ForumState";

  export default {
    name: "ViewQuestions",
    data () {
      return {
        forumState: this.ForumState.state,
        questionState: this.forumState.questionsToGet,
        userAccount: null,
        isStudent: true,
        userId: "",
        schools: [],
        departments: [],
        courses: [],
        response: '',
        questions: [ ],
        anwers: [ ]
      }
    },

    created() {
        // this.getSchoolQuestions()
    },

    watch: {
      questionState: function() {
          if(this.questionState == "school") {
              this.getSchoolQuestions()
          }
          else if(this.questionState == "department") {
              this.getDepartmentQuestions()
          }
          else if(this.questionState == "course") {
              this.getDepartmentQuestions()
          }
      }, 
    },
    
    // beforeMount(){
    //     this.userId = sessionStorage.SITuserId;
    //     this.getAccount()
    // },
    methods: {
      viewAnswers(item) {
            this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "GET", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/GetAnswers",
              params: {
                  questionId: item.QuestionId
              }
            })
              .then(response => {
                  this.answers = response.data;
                  this.ForumState.viewAnswers(item);
              })
              .catch(err => {
                  console.log(err);
              });
        },
      },
      postAnswer(item) {
        this.ForumState.openPostAnswerForm(item);
      },
      closeQuestion(qId) {
            const url = `${this.$hostname}DiscussionForum/CloseQuestion`;
            
            axios
            .post(url, {
                params:{
                    questionId: qId
                },
                headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
                
            })
            .then(response =>{
                this.item.IsClosed = response.data;
            })
            .catch(error =>{
                this.errorMessage = error.response.data.Message
            })
      },
      editQuestion(item) {
            this.ForumState.openEditQuestionForm(item); 
      },
      getSchoolQuestions() {
            this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "GET", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/GetQuestionsBySchool",
              params: {
                  // or id? if not change backend from id to name
                  schoolId: this.school
              }
            })
              .then(response => {
                  this.questions = response.data;
                  this.postedQuestion = true;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        getDepartmentQuestions() {
            this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "GET", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/GetQuestionsByDepartment",
              params: {
                  departmentId: this.department
              }
            })
              .then(response => {
                  this.questions = response.data;
                  this.postedQuestion = true;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        getCourseQuestions() {
            this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "GET", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/GetQuestionsByCourse",
              params: {
                  courseId: this.course
              }
            })
              .then(response => {
                  this.questions = response.data;
                  this.postedQuestion = true;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        getDraftQuestions() {
            this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "GET", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/DraftQuestions",
              // params: {
              // }
            })
              .then(response => {
                  this.questions = response.data;
                  this.postedQuestion = false;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        increaseQuestionSpamCount(qId) {
          this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "POST", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/IncreaseQuestionSpamCount",
              params: {
                  questionId: qId
              }
            })
              .then(response => {
                  this.item.SpamCount = response.data;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        increaseAnswerSpamCount(aId) {
          this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "POST", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/IncreaseAnswerSpamCount",
              params: {
                  answerId: aId
              }
            })
              .then(response => {
                  this.item.SpamCount = response.data;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        increaseAnswerHelpfulCount(aId) {
          this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "POST", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/IncreaseAnswerHelpfulCount",
              params: {
                  answerId: aId
              }
            })
              .then(response => {
                  this.item.HelpfulCount = response.data;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        increaseAnswerUnHelpfulCount(aId) {
          this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "POST", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/IncreaseAnswerUnHelpfulCount",
              params: {
                  answerId: aId
              }
            })
              .then(response => {
                  this.item.UnHelpfulCount = response.data;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        markAsCorrectAnswer(aId) {
          this.axios({
              headers: {
                //Accept: "application/json", 
                "Content-Type": "application/Json",
                //Authorization: "Bearer" + sessionStorage.SITtoken
              },
              method: "POST", 
              crossDomain: true,
              url: this.$hostname + "DiscussionForum/MarkAsCorrectAnswer",
              params: {
                  answerId: aId
              }
            })
              .then(response => {
                  this.item.UnHelpfulCount = response.data;
              })
              .catch(err => {
                  console.log(err);
              });
        },
    }
  
</script>

