<template>
<v-container>
    <template>
<!-- <v-app> -->
  <v-toolbar dark>
  <v-layout row>
  <v-toolbar-title centered>Discussion Forum </v-toolbar-title> 
    
    <v-flex shrink pa-1>
    <v-select
      v-model="school"
      :items="schools"
      label="School"
    ></v-select>
    </v-flex>
      
    <v-flex shrink pa-1>
    <v-select
        v-model="department"
        :items="departments"
        label="Department"
        @input="getDepartments"
    ></v-select>
    </v-flex>
            
    <v-flex shrink pa-1>
    <v-select
        v-model="course"
        :items="courses"
        label="Course"
        @input="getCourses"
    ></v-select>
    </v-flex>
    <v-spacer></v-spacer>

    <v-btn small @click="myDrafts()" > My Drafts </v-btn>    

    <v-btn small @click="postQuestion()" > Post </v-btn>

    <v-btn icon>
      <v-icon @click="viewQuestions()">refresh</v-icon>
    </v-btn>

  </v-layout>
  </v-toolbar>
  <!-- </v-app> -->
</template>










<template>
<v-container>
  <v-btn small @click="getSchoolQUestions()">School Questions</v-btn> 
  <v-btn small @click="getDepartmentQuestions()">Department Questions</v-btn> 
  <v-btn small @click="getCourseQuestions()">Course Questions</v-btn>
  <v-divider class ="ma-3"></v-divider>

<v-container>
<v-container v-if="postedQuestion">
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
        <v-btn small v-if="userId != item.AccountId" @click="postAnswer(item.QuestionId)">Post Answer</v-btn> {{"Exp needed to answer: " + item.ExpNeededToAnswer}}

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

<v-container v-if="!postedQuestion">
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

<v-container v-if="viewAnswers">
  <v-card class="mx-auto" dark color="pink" max-width="1400" max-height="800">

    <v-card-title>{{forumState.question.AccountName}}
      <v-spacer></v-spacer>
      {{forumState.question.DateCreated}}
    </v-card-title>
    
    <v-card-text class="headline font-weight-bold"> {{question.Text}} </v-card-text>

    <v-card-footer>{{ question.SchoolName + "/" + question.DepartmentName + "/" + question.CourseName }}</v-card-footer>
    
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

        <v-btn small v-if="userId == question.AccountId" @click="markAsCorrectAnswer(item.AnswerId)">Mark as Correct</v-btn> 

      </v-layout>
    </v-card-actions>

    <v-card-footer>{{ "Is Correct Ans: " + item.IsCorrectAnswer }}</v-card-footer>

    <v-divider class ="ma-3"></v-divider>
    
  </v-card>
</v-container>
</v-container>
</template>










<template>
  <v-dialog v-model="postQuestionForm" max-width="750">
    <!-- <div class="modal">  -->
      <!-- <v-container>     -->
      <v-card dark > 
        <h1>Post Question</h1>

        <v-form>
        <v-textarea
            name="text"
            id="text"
            type="text"
            v-model="textq"
            label="Enter question between 50 and 2000 characters"
            auto-grow
            v-if="!validation"
            rows="3"
            /><br />
        <v-text-field
            name="exp"
            id="exp"
            type="exp"
            v-model="expq"
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










<template>
  <v-dialog v-model="postAnswerForm" max-width="750">
    <!-- <div class="modal">  -->
      <!-- <v-container>     -->
      <v-card dark > 
        <h1>Post Answer</h1>

        <v-form>
        <v-textarea
            name="text"
            id="text"
            type="text"
            v-model="texta"
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











<template>
  <v-dialog v-model="editQuestionForm" max-width="750">
    <!-- <div class="modal">  -->
      <!-- <v-container>     -->
      <v-card dark > 
        <h1>Edit Question</h1>

        <v-form>
        <v-textarea
            name="text"
            id="text"
            type="text"
            v-model="texte"
            label="Enter question between 50 and 2000 characters"
            auto-grow
            v-if="!validation"
            rows="3"
            /><br />
        <v-text-field
            name="exp"
            id="exp"
            type="exp"
            v-model="expe"
            label="Enter exp a user needs to answer your question" 
            v-if="!validation"
            /><br />
        
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

        <v-btn id="btnPostQuestion" color="success" v-if="!validation" v-on:click="updateQuestion">Update Question</v-btn>

        <v-btn id="btnClose" color="grey" v-on:click="CloseEditQuestionForm()">Close</v-btn>

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
</v-container>
</template>










<script>
import axios from 'axios'
import ForumState from "@/services/ForumState";

  export default {
    name: "DFToolbar",
    data () {
      return {
        userAccount: null,
        isStudent: true,
        postedQuestion: true,
        viewAnswers: false,
        postQuestionForm: false,
        editQuestionForm: false,
        postAnswerForm: false,
        userId: "",
        school: "",
        schools: [],
        department: "",
        departments: [],
        course: "",
        courses: [],
        response: '',
        forumState: ForumState.state,
        questionState: ForumState.state.questionsToGet,
        viewPostedQuestions: ForumState.state.viewPostedQuestions,
        userAccount: null,
        isStudent: true,
        userId: "",
        schools: [],
        departments: [],
        courses: [],
        response: '',
        questions: [ ],
        anwers: [ ],
        forumState: ForumState.state,
      //postQuestionDialog: true,
      validation: null,
      text: '',
      exp: '',
      underMaintenance: false,
      error: '',
      loading: false,
       forumState: ForumState.state,
      //postQuestionDialog: true,
      validation: null,
      text: '',
      exp: '',
      error: '',
      loading: false,
      forumState: ForumState.state,
      validation: null,
      text: '',
      exp: '',
      error: '',
      loading: false,
      }
    },

    // created() {
    //     this.getSchoolQuestions()
    // },
    
    beforeMount(){
        this.userId = AppSession.state.userId;
        if(AppSession.state.category === "Student"){
            this.isStudent = true;
            this.school = AppSession.state.schoolId
            this.getDepartments(); 
        }
        else{
            this.getSchools();
        }
        //this.userId = "2"
    },
    methods: {
      // @Author: Krystal 
      
      getSchools: function(){
          this.errorMessage = "";

          const url = `${this.$hostname}search/selections`;
          Axios
          .get(url, {
              params:{
                  SearchCategory: 0
              },
              headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
              
          })
          .then(response =>{
              this.schools = response.data;
              if(this.schools.length > 0){
                  this.schools = [{id: 0, text: "NONE", value: 0 }].concat(this.schools)
              }
          })
          .catch(error =>{
              this.errorMessage = error.response.data.Message
          })
        
      },
      getDepartments: function(){
          this.errorMessage = "";

          const url = `${this.$hostname}search/selections`;
          Axios
          .get(url, {
              params:{
                  SearchCategory: 1,
                  SearchSchool: this.school
              },
              headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
              
          })
          .then(response =>{
              this.departments = response.data;
          })
          .catch(error =>{
              this.errorMessage = error.response.data.Message
          })
      },
      getCourses: function(){
          this.errorMessage = "";

          const url = `${this.$hostname}search/selections`;
          Axios
          .get(url, {
              params:{
                  SearchCategory: 2,
                  SearchSchool: this.school,
                  SearchDepartment: this.department
              },
              headers: { "Content-Type": "application/json", Authorization: "Bearer " + sessionStorage.SITtoken }
              
          })
          .then(response =>{
              this.courses = response.data;
          })
          .catch(error =>{
              this.errorMessage = error.response.data.Message
          })
      },
      // End Krytal

      postQuestion() {
        postQuestionForum = true;
        this.viewAnswers = false;
      },
      
      myDrafts() {
        this.postedQuestion = false;
        this.viewAnswers = false;
        getDraftQuestions()
      },

      viewQuestions() {
        this.postedQuestion = true;
      },
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
                  questionId: qId
              }
            })
              .then(response => {
                  this.answers = response.data;
                  this.question = item;
                  this.postedQuestion = false;
                  this.viewAnswers = true;
              })
              .catch(err => {
                  console.log(err);
              });
        },
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
            ForumState.openEditQuestionForm(item); 
      },
      getQuestions() {
            if(questionState == "school") {
              getSchoolQuestions()
          }
          else if(questionState == "department") {
              getDepartmentQuestions()
          }
          else if(questionState == "course") {
              getDepartmentQuestions()
          }
      },
      getSchoolQuestions() {
          this.postedQuestion = true;
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
                  this.viewAnswers = false;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        getDepartmentQuestions() {
            this.postedQuestion = true;
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
                  this.viewAnswers = false;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        getCourseQuestions() {
            this.postedQuestion = true;
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
                  this.viewAnswers = false;
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
              url: this.$hostname + "DiscussionForum/GetDraftQuestions",
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
         ClosePostQuestionForm() {
      this.postQuestionForum = false;
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
        SchoolId: this.school,
        //SchoolId: "1",        
        DepartmentId: this.department,
        CourseId: this.course,
        Text: this.textq,
        Exp: this.expq.toString(),
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
      this.axios.post(this.$hostname + url, {
         
        //'Send': 'application/json'
          QuestionType: "DraftQuestion",
          AccountId: sessionStorage.SITuserId,
          Text: this.textq,
          Exp: this.expq.toString(),
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
          this.postQuestionForum = false;
        })
    },
    CloseEditQuestionForm() {
      this.editQuestionForm;
    },
    updateQuestion(qId) {
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

      const url = 'DiscussionForum/UpdateQuestion'
      this.loading = true;
      this.axios.post(this.$hostname + url, {
        QuestionId: qId,
        AccountId: sessionStorage.SITuserId,
        Text: this.texte,
        Exp: this.expe.toString(),
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
          this.editQuestionForm = false;
        })
    },
    ClosePostAnswerForm() {
      this.postAnswerForm = false;
    },
    postAnswer(qid) {
      this.error = "";

      const url = 'DiscussionForum/PostAnswer'
      this.loading = true;
      this.axios.post(this.$hostname + url, {
        QuestionId: qid,
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
          ForumState.closePostAnswerForm()
        })
    },

    }
  
</script>

