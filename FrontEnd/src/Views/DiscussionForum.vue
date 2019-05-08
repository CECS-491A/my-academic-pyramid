<template>
<v-app>
    <v-container fluid dark>
    <v-layout column>
      <v-card dark fluid>
      <v-layout column>
        <v-toolbar dark>
          <v-layout row>
          <v-toolbar-title centered>Discussion Forum   </v-toolbar-title> 
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

            <v-btn small> Post </v-btn>

            <v-btn icon>
              <v-icon>refresh</v-icon>
            </v-btn>

          </v-layout>
        </v-toolbar>

        <v-card fluid>
          <v-toolbar color="pink" dark>
            <v-toolbar-side-icon @click="drawer =!drawer"></v-toolbar-side-icon>
            <v-toolbar-title v-if="!errorMessage">Questions</v-toolbar-title>
            <v-spacer></v-spacer>
          </v-toolbar>

          <!-- <v-navigation-drawer v-resize directive>
          <v-layout column align-center>
            <v-flex>
            <p>User Name</p>
            </v-flex>
          </v-layout>
          <v-list>
            <v-list-tile v-for="link in links" :key="link.name" router :to="link.route">
              <v-list-tile-content>
                <v-list-tile-title>{{link.text}}</v-list-tile-title>
              </v-list-tile-content>
            </v-list-tile>
          </v-list>
          </v-navigation-drawer> -->

        </v-card>

        
        



        </v-layout>
      </v-card>  
    </v-layout>

    <v-list three-line dark>
          <!-- <template v-for="(item, index) in questions"> -->
            <template v-for="item in questions">
            <v-list-tile
              :key="item.Id"
              avatar
              ripple
              @click=""
            >
              <v-list-tile-content>
                <v-list-tile-title>{{ item.AccountName }}</v-list-tile-title>
                <v-list-tile-sub-title class="text--primary">{{ item.Text }}</v-list-tile-sub-title>
                <v-list-tile-sub-title>{{ item.SchoolName + "/" + item.DepartmentName + "/" + item.CourseName 
                    + " Spam Count: " + item.SpamCount + " Answers: " + item.AnswerCount + " Exp Needed to Answer: " + item.ExpNeededToAnswer}}</v-list-tile-sub-title>
              </v-list-tile-content>

              <v-list-tile-action>
                <v-list-tile-action-text>{{ item.DateCreated }}</v-list-tile-action-text>
                
                <div>
                  <v-btn flat icon color="green lighten-2">
                    <v-icon>thumb_up</v-icon>
                  </v-btn>

                  <v-btn flat icon color="red lighten-2">
                    <v-icon>thumb_down</v-icon>
                    </v-btn>
                </div>

                <div>
                  <v-btn small>See Answers</v-btn>
                  <v-btn small>Post Answer</v-btn>
                </div>
              </v-list-tile-action>

            </v-list-tile>
            <v-divider
              v-if="index + 1 < questions.length"
              :key="index"
            ></v-divider>
          </template>
        </v-list>

    <!-- <div>
        <v-alert
            :value="errorMessage"
            id="errorMessage"
            type="error"
            transition="scale-transition"
        >
            {{errorMessage}}
        </v-alert>
    </div> -->

  </v-container>
</v-app>

</template>

<script>
import axios from 'axios'

  export default {
    data () {
      return {
        //selected: [2],
        selected: 'A',
            options: [
                { text: 'One', value: 'A' },
                { text: 'Two', value: 'B' },
                { text: 'Three', value: 'C' }
            ],
            questions: [],
            schools: [
              'All', 'Family', 'Friends', 'Coworkers'
            ],
            departments: [
              'All', 'Family', 'Friends', 'Coworkers'
            ],
            courses: [
              'All', 'Family', 'Friends', 'Coworkers'
            ],
            drawer: false,
            links: [
                { name: 'Questions', text: 'Questions', route: '/DiscussionForum'},
                { name: 'Drafts', text: 'Drafts', route: '/DiscussionForum'}
            ],
            response: ''
        // questions: [
        //   {
        //     questionId: '',
        //     schoolName: '',
        //     departmentName: '',
        //     courseName: '',
        //     accountId: "",
        //     accountName: "",
        //     text: "",
        //     expNeededToAnswer: "",
        //     isClosed: "",
        //     spamCount: "",
        //     answerCount: "",
        //     dateCreated: "",
        //     dateUpdated: "",
        //   }
        //]
      }
    },
    created() {
        this.getSchoolQuestions()
    },
    methods: {
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
                  schoolId: 1
              }
            })
              .then(response => {
                  this.questions = response.data;
              })
              .catch(err => {
                  console.log(err);
              });
        },
        // toggle (index) {
        //   const i = this.selected.indexOf(index)
        //   if (i > -1) {
        //     this.selected.splice(i, 1)
        //   } else {
        //     this.selected.push(index)
        //   }
        // }
        // beforeMount(){
    //     this.userId = sessionStorage.SITuserId;
    //     //this.userId = "2"
    //     this.getAccount()
    // }
    }
  }
</script>

