
<template>
  <div id="schoolRegistration">
    <v-app id="schoolRegistration">
      <v-card>
        <v-toolbar>
          <v-toolbar-title>Create User</v-toolbar-title>
        </v-toolbar>
		<v-tabs left color="cyan" dark icons-and-text>
	<v-tabs-slider color="green"></v-tabs-slider>
	<v-tab id="JSONfile">File Upload </v-tab>
	<v-tab-item>
		_<v-card-title primary-title>
            <div>
              <h3 class="headline mb-0">File Content</h3>
              <div> {{ school.SchoolName }} </div>
			  
            </div>
          </v-card-title>


    <FileReader @load="text = $event"></FileReader>
			  {{school.SchoolName}}
	</v-tab-item>
	<v-tab id="ManualEnter">Enter manually</v-tab>
	<v-tab-item>
		   <v-card-text>
          <form>
            <v-text-field
              id="schoolName"
              v-model="school.SchoolName"
              :error-messages="textErrors"
              label="School Name"
              required
              @input="$v.schoolName.$touch()"
              @blur="$v.schoolName.$touch()"
            ></v-text-field>

            <v-text-field
              id="schoolContactEmail"
              v-model="school.SchoolContactEmail"
              :error-messages="emailErrors"
              label="School Contact Email"
              required
              @input="$v.school.SchoolContactEmail.$touch()"
              @blur="$v.school.SchoolContactEmail.$touch()"
            ></v-text-field>

            <v-text-field
              id="schoolEmailDomain"
              v-model="school.SchoolEmailDomain"
              label="School Email Domain"
              required
              @input="$v.formData.LastName.$touch()"
              @blur="$v.formData.LastName.$touch()"
            ></v-text-field>

         

			

            <div v-for="item in teachers"
			:key="item.FirstName">
			{{item.FirstName}}
			{{item.LastName}}
			{{item.MiddleName}}
			{{item.DepartmentName}}
			</div>

            <v-btn @click="submitData">submit</v-btn>
            <v-alert
              :value="failedAlert"
              type="error"
              transition="scale-transition"
            >Submit Failed. Please check all fields</v-alert>

            <v-alert
              :value="successAlert"
              type="success"
              transition="scale-transition"
            >Submit Sucessfully</v-alert>

			

            <v-btn @click="clear">clear</v-btn>
          </form>
        </v-card-text>
		<courseTable></courseTable>
			<teacherTable></teacherTable>
	</v-tab-item>
</v-tabs>
     
      </v-card>
    </v-app>
  </div>
</template>



<script>
/*global console*/ /* eslint no-console: "off" */
import { validationMixin } from "vuelidate";

import { required, maxLength, email } from "vuelidate/lib/validators";
import courseTable from "@/components/SchoolRegistration/CourseTable";
import teacherTable from "@/components/SchoolRegistration/TeacherTable";
import FileReader from "@/components/SchoolRegistration/FileReader";

export default {
  mixins: [validationMixin],

  validations: {
    school: {
      SchoolName: { required, maxLength: maxLength(10) },
      SchoolContactEmail: { required, email },

    }
  },
  components:{
	  courseTable,
	  teacherTable,
	  FileReader
  },

  data: () => ({
    school: {
      SchoolName: "",
      SchoolContactEmail: "",
      SchoolEmailDomain: ""
    },

    departments: {
      DepartmentName: ""
    },

    departmentItems: [],

    courses: [{
      CourseName: "",
      SchoolName: "",
      DepartmentName: "",
      TeacherFirstName: "",
      TeacherLastName: ""
    }],

    teachers: [{
      FirstName: "",
      MiddleName: "",
      LastName: "",
      DepartmentName: ""
	}],
	
	text:""
  }),

  computed: {
    textErrors() {
      const errors = [];

      if (!this.$v.school.SchoolName.$dirty) return errors;

      !this.$v.school.SchoolName.maxLength &&
        errors.push("Name must be at most 10 characters long");

      !this.$v.school.SchoolName.required &&
        errors.push("Last Name is required.");

      return errors;
    },

    emailErrors() {
      const errors = [];

      if (!this.$v.school.SchoolContactEmail.$dirty) return errors;

      !this.$v.school.SchoolContactEmail.email && errors.push("Must be valid e-mail");

      !this.$v.school.SchoolContactEmail.required && errors.push("E-mail is required");

      return errors;
    }
  },
  created() {
    this.$eventBus.$on("SendCourseTable", courses => {
      this.courses = courses
	}),
	this.$eventBus.$on("SendTeacherTable", teachers => {
      this.teachers = teachers
	})
	
  },
  watch: {
    DateOfBirth(val) {
      val && setTimeout(() => (this.$refs.picker.activePicker = "YEAR"));
	},
	text(){
		this.convertData()
	}
  },
  methods: {
    submitData() {
     
  },
  convertData(){
	  this.data = JSON.parse(this.text)
  }

  }
};
</script>

