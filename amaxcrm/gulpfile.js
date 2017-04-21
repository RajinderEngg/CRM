var gulp = require('gulp');
var crmProd = 'C:/inetpub/wwwroot/crm/';

var assetsDev = 'assets/';
var assetsProd = 'src/';

var appDev = 'dev/';
var appProd = 'app/';

/* Mixed */
var ext_replace = require('gulp-ext-replace');

/* CSS */
var postcss = require('gulp-postcss');
var sourcemaps = require('gulp-sourcemaps');
var autoprefixer = require('autoprefixer');
var precss = require('precss');
var cssnano = require('cssnano');

/* JS & TS */
var jsuglify = require('gulp-uglify');
var typescript = require('gulp-typescript');

/* Images */
var imagemin = require('gulp-imagemin');

/* Jsones */
var jsonminify = require('gulp-jsonminify');



var tsProject = typescript.createProject('./tsconfig.json');

gulp.task('build-css', function () {
    return gulp.src(assetsDev + 'scss/*.scss')
        .pipe(sourcemaps.init())
        .pipe(postcss([precss, autoprefixer, cssnano]))
        .pipe(sourcemaps.write())
        .pipe(ext_replace('.css'))
        .pipe(gulp.dest(assetsProd + 'css/'));
});

gulp.task('build-ts', function () {
    return gulp.src(appDev + '**/*.ts')
        .pipe(sourcemaps.init())
        .pipe(typescript(tsProject))
        .pipe(sourcemaps.write())
        //.pipe(jsuglify())
        .pipe(gulp.dest(appProd));
});

gulp.task('build-img', function () {
    return gulp.src(assetsDev + 'img/**/*')
        .pipe(imagemin({
            progressive: true
        }))
        .pipe(gulp.dest(assetsProd + 'img/'));
});

gulp.task('build-json', function () {
    return gulp.src(assetsDev + '**/*.json')
        .pipe(jsonminify())
        .pipe(gulp.dest(assetsProd));
});

gulp.task('build-html', function () {
    return gulp.src(appDev + '**/*.html')
        .pipe(gulp.dest(appProd));
});

gulp.task('build-bundle',function () {
    return gulp.src('src_bundle/**/*')
    .pipe(gulp.dest(assetsProd));
});

gulp.task('watch', function () {
    gulp.watch(appDev + '**/*.ts', ['build-ts']);
    gulp.watch(appDev + '**/*.html', ['build-html']);
    gulp.watch(assetsDev + 'scss/**/*.scss', ['build-css']);
    gulp.watch(assetsDev + 'img/*', ['build-img']);
    gulp.watch(assetsDev + '**/*.json', ['build-json']);

});

gulp.task('default', ['build-ts', 'build-css','build-bundle','build-html','build-json','watch']);

gulp.task('build', ['build-ts', 'build-css','build-bundle','build-html', 'build-json']);




//Build crm
var dependencyFiles=[
    '*.html',
    'node_modules/es6-shim/es6-shim.min.js',
    'node_modules/systemjs/dist/system-polyfills.js',
    'node_modules/angular2/es6/dev/src/testing/shims_for_IE.js',
    'node_modules/angular2/bundles/angular2-polyfills.js',
    'node_modules/systemjs/dist/system.src.js',
    'node_modules/rxjs/bundles/Rx.js',
    'node_modules/angular2/bundles/angular2.js',
    'node_modules/angular2/bundles/router.dev.js',
    'node_modules/angular2/bundles/http.js'
];

gulp.task('build-app-src',function () {
    return gulp.src(assetsProd+"**/*")
    .pipe(gulp.dest(crmProd+assetsProd));
});
gulp.task('build-app-app',function () {
    return gulp.src(appProd+"**/*")
    .pipe(gulp.dest(crmProd+appProd));
});
gulp.task('build-app-copyfiles',function () {
    for(var i=0;i<dependencyFiles.length;i++)
    if(dependencyFiles[i].indexOf('/')>=0)
        gulp.src(dependencyFiles[i]).pipe(gulp.dest(crmProd+(dependencyFiles[i].substr(0,dependencyFiles[i].lastIndexOf('/')+1))));
    else
        gulp.src(dependencyFiles[i]).pipe(gulp.dest(crmProd));
});

gulp.task('publish',['build-app-src','build-app-app','build-app-copyfiles']);


gulp.task('comp',function () {
    gulp.runTask('build-ts')
    gulp.watch(appDev + '**/*.ts', ['build-ts']);
    gulp.watch(assetsDev + '**/*.json', ['build-json']);
})