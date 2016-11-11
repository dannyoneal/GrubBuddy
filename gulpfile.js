const gulp = require('gulp');
const del = require('del');
const typescript = require('gulp-typescript');
const tscConfig = require('./tsconfig.json');
const sourcemaps = require('gulp-sourcemaps');

// clean the contents of the distribution directory
gulp.task('clean', function () {
  return del('dist/**/*');
});

// TypeScript compile
gulp.task('compile', ['clean'], function () {
  return gulp
    .src('client/**/*.ts')
    .pipe(sourcemaps.init())          
    .pipe(typescript(tscConfig.compilerOptions))
    .pipe(sourcemaps.write('.'))      
    .pipe(gulp.dest('dist/app'));
});

// copy dependencies
gulp.task('copy:libs', ['clean'], function() {
  return gulp.src([
    'client/node_modules/core-js/client/shim.min.js',
    'client/node_modules/zone.js/dist/zone.js',
    'client/node_modules/reflect-metadata/Reflect.js',
    'client/node_modules/systemjs/dist/system.src.js'
    ])
    .pipe(gulp.dest('dist/lib'))
});

// copy static assets - i.e. non TypeScript compiled source
gulp.task('copy:assets', ['clean'], function() {
  return gulp.src(['client/**/*', 'index.html', 'styles.css', '!app/**/*.ts'], { base : './' })
    .pipe(gulp.dest('dist'))
});

gulp.task('build', ['compile', 'copy:libs', 'copy:assets']);
gulp.task('default', ['build']);